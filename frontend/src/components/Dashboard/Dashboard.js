import React, { useEffect, useRef, useState } from 'react';
import Chart from 'chart.js/auto'
import useAuth  from '../../hooks/useAuth';

const Dashboard = () => {
    const earningsChartRef = useRef(null);
    const assetsChartRef = useRef(null);
    const earningsChartInstance = useRef(null);
    const assetsChartInstance = useRef(null);
    const [portfolio, setPortfolio] = useState(null);
    const [user, setUser] = useState(null);
    const { auth } = useAuth();
    const [portfolioValues, setPortfolioValues] = useState([]);
    const [portfolioComponents, setPortfolioComponents] = useState([]);
    const [accountBalance, setAccountBalance] = useState(null);

    useEffect(() => {
            const fetchAccountBalance = async () => {
                try {
                    const response = await fetch(`http://localhost:5000/api/AccountBalance/${portfolio.id}`, {
                        headers: {
                            "Authorization": `Bearer ${auth.token}`,
                            "Content-Type": "application/json"
                        }
                    });
                    if (!response.ok) throw new Error("Error fetching account balance");
                    const balance = await response.json();
                    setAccountBalance(balance);
                } catch (error) {
                    console.error("Error fetching account balance", error);
                }
            };

            fetchAccountBalance();
        }, [portfolio]
    )

    useEffect(() => {
        const fetchPortfolioComponents = async () => {
            try {
                const response = await fetch(`http://localhost:5000/api/Portfolio/portfolio-components/${portfolio.id}`,
                    {
                        headers: {
                            "Authorization": `Bearer ${auth.token}`,
                            "Content-Type": "application/json"
                        }
                    }
                );
                if (!response.ok) throw new Error("Błąd pobierania danych");
                const data = await response.json();
                setPortfolioComponents(data);
            } catch (error) {
                console.error("Błąd:", error);
            }
        };

        fetchPortfolioComponents();
    }, [portfolio]);

    useEffect(() => {
        const fetchPortfolioValues = async () => {
            try {
                console.log(portfolio);
                const response = await fetch(`http://localhost:5000/api/Portfolio/portfolio-value/${portfolio.id}`,
                    {
                        headers: {
                            "Authorization": `Bearer ${auth.token}`,
                            "Content-Type": "application/json"
                        }
                    }
                );
                if (!response.ok) throw new Error("Błąd pobierania danych");
                const data = await response.json();
                setPortfolioValues(data);
            } catch (error) {
                console.error("Błąd:", error);
            }
        };

        fetchPortfolioValues();
    }, [portfolio]);
    
    
    useEffect(() => {
        const fetchUser = async () => {
            try {
                const response = await fetch("http://localhost:5000/User/me", {
                    headers: { 
                        "Authorization": `Bearer ${auth.token}`, 
                        "Content-Type": "application/json"
                    }
                });
                if (!response.ok) throw new Error("User not found");
                const userId = await response.text();
                setUser({id : userId});
            } catch (error) {
                setUser(null);
            }
        };
        fetchUser();
    }, []);

    useEffect(() => {
        if (user) {
            const fetchPortfolio = async () => {
                try {
                    const response = await fetch(`http://localhost:5000/api/Portfolio/${user.id}`,{
                        headers: {
                            "Authorization": `Bearer ${auth.token}`,
                                "Content-Type": "application/json"
                        }
                    });
                    if (!response.ok) throw new Error("Portfolio not found");
                    const data = await response.json();
                    setPortfolio(data);
                } catch (error) {
                    setPortfolio(null);
                }
            };

            fetchPortfolio();
        }else{
            console.log('User not found');
        }
    }, [user]);

    const createPortfolio = async () => {
        try {
            console.log(user);
            const response = await fetch("http://localhost:5000/api/Portfolio", {
                method: "POST",
                headers: {
                    "Authorization": `Bearer ${auth.token}`,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ userId: user.id, name: "My Portfolio" })
            });
            if (!response.ok) throw new Error("Failed to create portfolio");
            const data = await response.json();
            setPortfolio(data);
        } catch (error) {
            console.error("Error creating portfolio:", error);
        }
    };

    useEffect(() => {
        if (portfolio) {
            if (earningsChartRef.current) {
                if (earningsChartInstance.current) earningsChartInstance.current.destroy();
                earningsChartInstance.current = new Chart(earningsChartRef.current, {
                    type: 'line',
                    data: {
                        labels: portfolioValues.map((pv) => `${pv.monthName} ${pv.year}`),
                        datasets: [
                            {
                                label: "Total Value",
                                data: portfolioValues.map((pv) => pv.totalValue),
                                backgroundColor: "rgba(78, 115, 223, 0.05)",
                                borderColor: "rgba(78, 115, 223, 1)",
                                fill: true,
                            },
                        ],
                    },
                    options: { maintainAspectRatio: false, plugins: { legend: { display: false } } }
                });
            }

            if (assetsChartRef.current) {
                if (assetsChartInstance.current) assetsChartInstance.current.destroy();
                const labels = Object.keys(portfolioComponents); 
                const data = Object.values(portfolioComponents); 
                assetsChartInstance.current = new Chart(assetsChartRef.current, {
                    type: 'doughnut',
                    data: {
                        labels: labels,
                        datasets: [{
                            data: data,
                            backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
                            borderColor: ['#ffffff', '#ffffff', '#ffffff']
                        }]
                    },
                    options: { maintainAspectRatio: false, plugins: { legend: { display: true, position: 'right' } } }
                });
            }
        }
    }, [portfolio, portfolioComponents, portfolioValues]);

    return (
        <>
            <DashboardHeader />
            {!portfolio ? (
                <div className="text-center p-4">
                    <p>No portfolio found. Please create one.</p>
                    <button className="btn btn-primary" onClick={createPortfolio}>Create Portfolio</button>
                </div>
            ) : (
                <>
                    <EarningsCards accountBalance={accountBalance}/>
                        <EarningsOverview chartRef={earningsChartRef} />
                        <AssetsOverview chartRef={assetsChartRef} />
                    <ProfitSection />
                </>
            )}
        </>
    );
};

const DashboardHeader = () => (
    <div className="d-sm-flex justify-content-between align-items-center mb-4">
        <h3 className="text-dark mb-0">Dashboard</h3>
    </div>
);

const EarningsCards = ({ accountBalance }) => (
    <div className="row">
        <div className="col-md-6 col-xl-3 mb-4">
            <div className="card shadow border-left-primary py-2">
                <div className="card-body">
                    <div className="text-dark fw-bold h5 mb-0"><span>${accountBalance !== null ? accountBalance.toFixed(2) : "Loading..."}</span></div>
                </div>
            </div>
        </div>
    </div>
);

const EarningsOverview = ({ chartRef }) => (
    <div className="col-12 mb-4">
        <div className="card shadow mb-4">
            <div className="card-header py-3">
                <h6 className="m-0 font-weight-bold text-primary">Earnings Overview</h6>
            </div>
            <div className="card-body">
                <div style={{ height: '300px' }}>
                    <canvas ref={chartRef}></canvas>
                </div>
            </div>
        </div>
    </div>
);

const AssetsOverview = ({ chartRef }) => (
    <div className="col-12 mb-4">
        <div className="card shadow mb-4">
            <div className="card-header py-3">
                <h6 className="m-0 font-weight-bold text-primary">Assets Overview</h6>
            </div>
            <div className="card-body">
                <div style={{ height: '300px' }}>
                    <canvas ref={chartRef}></canvas>
                </div>
            </div>
        </div>
    </div>
);

const ProfitSection = () => (
    <div className="row">
        <div className="col-12 mb-4">
            <div className="card shadow mb-4">
                <div className="card-body">
                    <h4 className="small fw-bold">Crypto<span className="float-end">60%</span></h4>
                    <div className="progress mb-4">
                        <div className="progress-bar bg-primary" role="progressbar" style={{ width: '60%' }}></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
);

export default Dashboard;
