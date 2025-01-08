import React, { useEffect, useRef } from 'react';
import Chart from 'chart.js/auto';
// import { Calendar, DollarSign, Download } from "lucide-react";
// import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip } from 'recharts';
// import { PieChart, Pie, Cell } from 'recharts';

// import '../../assets/bootstrap/css/bootstrap.min.css';  
// import '../../assets/fonts/fontawesome-all.min.css';  
const Dashboard = () => {
    const earningsChartRef = useRef(null);
    const assetsChartRef = useRef(null);
    const earningsChartInstance = useRef(null);
    const assetsChartInstance = useRef(null);

    useEffect(() => {
        if (earningsChartRef.current) {
            // Destroy existing chart if it exists
            if (earningsChartInstance.current) {
                earningsChartInstance.current.destroy();
            }
            // Earnings Chart
            earningsChartInstance.current = new Chart(earningsChartRef.current, {
                type: 'line',
                data: {
                    labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug'],
                    datasets: [{
                        label: 'Earnings',
                        data: [0, 10000, 5000, 15000, 10000, 20000, 15000, 25000],
                        backgroundColor: 'rgba(78, 115, 223, 0.05)',
                        borderColor: 'rgba(78, 115, 223, 1)',
                        fill: true
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: {
                        legend: { display: false }
                    }
                }
            });
        }

        if (assetsChartRef.current) {
            // Destroy existing chart if it exists
            if (assetsChartInstance.current) {
                assetsChartInstance.current.destroy();
            }
            // Assets Chart
            assetsChartInstance.current = new Chart(assetsChartRef.current, {
                type: 'doughnut',
                data: {
                    labels: ['Crypto', 'Stocks', 'Bonds'],
                    datasets: [{
                        data: [50, 30, 15],
                        backgroundColor: ['#4e73df', '#1cc88a', '#36b9cc'],
                        borderColor: ['#ffffff', '#ffffff', '#ffffff']
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: {
                        legend: { display: false }
                    }
                }
            });
        }

        // Cleanup on component unmount
        return () => {
            if (earningsChartInstance.current) {
                earningsChartInstance.current.destroy();
            }
            if (assetsChartInstance.current) {
                assetsChartInstance.current.destroy();
            }
        };
    }, []);
    return (
        <>
            <DashboardHeader />
            <EarningsCards />
            <div className="row">
                <EarningsOverview chartRef={earningsChartRef} />
                <AssetsOverview chartRef={assetsChartRef} />
            </div>
            <ProfitSection />
        </>
    );
};

const DashboardHeader = () => (
    <div className="d-sm-flex justify-content-between align-items-center mb-4">
        <h3 className="text-dark mb-0">Dashboard</h3>
        <a className="btn btn-primary btn-sm d-none d-sm-inline-block" role="button" href="#">
            <i className="fas fa-download fa-sm text-white-50"></i>&nbsp;Generate Report
        </a>
    </div>
);

const EarningsCards = () => (
    <div className="row">
        <div className="col-md-6 col-xl-3 mb-4">
            <div className="card shadow border-left-primary py-2">
                <div className="card-body">
                    <div className="row g-0 align-items-center">
                        <div className="col me-2">
                            <div className="text-uppercase text-primary fw-bold text-xs mb-1">
                                <span>Earnings (monthly)</span>
                            </div>
                            <div className="text-dark fw-bold h5 mb-0"><span>$40,000</span></div>
                        </div>
                        <div className="col-auto"><i className="fas fa-calendar fa-2x text-gray-300"></i></div>
                    </div>
                </div>
            </div>
        </div>
        <div className="col-md-6 col-xl-3 mb-4">
            <div className="card shadow border-left-success py-2">
                <div className="card-body">
                    <div className="row g-0 align-items-center">
                        <div className="col me-2">
                            <div className="text-uppercase text-success fw-bold text-xs mb-1">
                                <span>Earnings (annual)</span>
                            </div>
                            <div className="text-dark fw-bold h5 mb-0"><span>$215,000</span></div>
                        </div>
                        <div className="col-auto"><i className="fas fa-dollar-sign fa-2x text-gray-300"></i></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
);

const EarningsOverview = ({ chartRef }) => (
    <div className="col-lg-7 col-xl-8">
        <div className="card shadow mb-4">
            <div className="card-header d-flex justify-content-between align-items-center">
                <h6 className="text-primary fw-bold m-0">Earnings Overview</h6>
            </div>
            <div className="card-body">
                <div className="chart-area">
                    <canvas ref={chartRef}></canvas>
                </div>
            </div>
        </div>
    </div>
);

const AssetsOverview = ({ chartRef }) => (
    <div className="col-lg-5 col-xl-4">
        <div className="card shadow mb-4">
            <div className="card-header d-flex justify-content-between align-items-center">
                <h6 className="text-primary fw-bold m-0">Assets</h6>
            </div>
            <div className="card-body">
                <div className="chart-area">
                    <canvas ref={chartRef}></canvas>
                </div>
                <div className="text-center small mt-4">
                    <span className="me-2"><i className="fas fa-circle text-primary"></i>Crypto</span>
                    <span className="me-2"><i className="fas fa-circle text-success"></i>Stocks</span>
                    <span className="me-2"><i className="fas fa-circle text-info"></i>&nbsp;Bonds</span>
                </div>
            </div>
        </div>
    </div>
);

const ProfitSection = () => (
    <div className="row">
        <div className="col-lg-6 mb-4">
            <div className="card shadow mb-4">
                <div className="card-header py-3" style={{ color: 'rgb(22,33,119)', background: 'var(--bs-navbar-color)', borderColor: 'rgb(67,73,117)' }}>
                    <h6 className="text-primary fw-bold m-0">Profit</h6>
                </div>
                <div className="card-body">
                    <h4 className="small fw-bold">Bonds<span className="float-end">8%</span></h4>
                    <div className="progress mb-4">
                        <div className="progress-bar bg-info" role="progressbar" style={{ width: '8%' }} aria-valuenow="8" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <h4 className="small fw-bold">Stocks<span className="float-end">15%</span></h4>
                    <div className="progress mb-4">
                        <div className="progress-bar bg-success" role="progressbar" style={{ width: '15%' }} aria-valuenow="15" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                    <h4 className="small fw-bold">Crypto<span className="float-end">60%</span></h4>
                    <div className="progress mb-4">
                        <div className="progress-bar bg-primary" role="progressbar" style={{ width: '60%' }} aria-valuenow="60" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
);

export default Dashboard;