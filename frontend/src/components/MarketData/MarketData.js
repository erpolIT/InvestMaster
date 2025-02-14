import { useState, useEffect } from "react";
import { Modal, Button, Form } from "react-bootstrap";
import useAuth from "../../hooks/useAuth";

const MarketData = () => {
    const [data, setData] = useState({ cryptocurrencies: [], stocks: [], etfs: [] });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [selectedAsset, setSelectedAsset] = useState(null);
    const [quantity, setQuantity] = useState(1);
    const [showModal, setShowModal] = useState(false);
    const [userPortfolioId, setUserPortfolioId] = useState(null);
    const [investments, setInvestments] = useState([]);
    const [transactionType, setTransactionType] = useState(null);
    const { auth } = useAuth();
    


    useEffect(() => {
        const fetchMarketData = async () => {
            try {
                const response = await fetch("http://localhost:5000/api/Asset/all");
                if (!response.ok) throw new Error("Error fetching data");
                const result = await response.json();
                setData({
                    stocks: result.filter(asset => asset.assetTypeId === 1),
                    cryptocurrencies: result.filter(asset => asset.assetTypeId === 2),
                    etfs: result.filter(asset => asset.assetTypeId === 3)
                });
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };
        fetchMarketData();
    }, []);

    useEffect(() => {
        const fetchUserPortfolioAndInvestments = async () => {
            if (!auth?.token) return;
            try {
                const response = await fetch("http://localhost:5000/User/me", {
                    headers: {
                        "Authorization": `Bearer ${auth.token}`,
                        "Content-Type": "application/json"
                    }
                });
                if (!response.ok) throw new Error("User not found");
                const userId = await response.text();

                const portfolioResponse = await fetch(`http://localhost:5000/api/Portfolio/user-portfolio/${userId}`, {
                    headers: {
                        "Authorization": `Bearer ${auth.token}`,
                        "Content-Type": "application/json"
                    }
                });
                if (!portfolioResponse.ok) throw new Error("Portfolio not found");
                const portfolioId = await portfolioResponse.json();
                setUserPortfolioId(portfolioId);

                const investmentsResponse = await fetch(`http://localhost:5000/api/Investment/portfolio-investments/${portfolioId}`);
                if (!investmentsResponse.ok) throw new Error("Investments not found");
                const investmentsData = await investmentsResponse.json();
                setInvestments(investmentsData);
            } catch (error) {
                setError(error.message);
            }
        };
        fetchUserPortfolioAndInvestments();
    }, [auth]);


    const createInvestment = async (assetId) => {
        try {
            const response = await fetch("http://localhost:5000/api/Investment/add-investment", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ portfolioId: userPortfolioId, assetId })
            });
            if (!response.ok) throw new Error("Failed to create investment");
            return await response.json();
        } catch (error) {
            console.error("Error creating investment", error);
            return null;
        }
    };
    
    const handlePurchase = async (type) => {
        if (!userPortfolioId) {
            console.error("Portfolio ID not available");
            return;
        }
        
        let investment = investments.find(inv => inv.assetId === selectedAsset.id);
        if (!investment && type == "BUY") {
            const newInvestment = await createInvestment(selectedAsset.id);
            if (!newInvestment) return;
            setInvestments([...investments, newInvestment]);
            investment = newInvestment
        }

        const transactionRequest = {
            InvestmentId: investment.id,
            PortfolioId: userPortfolioId,
            Type: transactionType,
            Quantity: quantity,
            Price: selectedAsset.currentPriceClose,
            Fee: 0,
            Notes: ""
        };

        try {
            const response = await fetch("http://localhost:5000/api/Transaction/create", {
                method: "POST",
                headers: { 
                    "Authorization": `Bearer ${auth.token}`,
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(transactionRequest)
            });

            if (!response.ok) throw new Error("Transaction failed");
            console.log("Transaction successful");
        } catch (error) {
            console.error("Error processing transaction", error);
        }

        setShowModal(false);
    };

    if (loading) return <div className="text-center p-4">Loading...</div>;
    if (error) return <div className="alert alert-danger p-4">{error}</div>;

    const AssetSection = ({ title, assets }) => (
        <div className="card shadow-sm mb-4">
            <div className="card-header bg-white">
                <h5 className="card-title mb-0 text-primary">{title}</h5>
            </div>
            <div className="card-body p-0">
                <table className="table table-hover mb-0">
                    <tbody>
                    {assets.map((asset) => (
                        <tr key={asset.symbol}>
                            <td className="fw-bold">{asset.symbol}</td>
                            <td className="text-end">${asset.currentPriceClose.toFixed(2)}</td>
                            <td className="text-end">
                                <button className="btn btn-success btn-sm" onClick={() => handleBuyClick(asset)}>Buy</button>
                                <button className="btn btn-danger btn-sm ms-2" onClick={() => handleSellClick(asset)}>Sell</button>
                            </td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </div>
        </div>
    );

    const handleBuyClick = (asset) => {
        setSelectedAsset(asset);
        setTransactionType("BUY");
        setQuantity(1);
        setShowModal(true);
    };

    const handleSellClick = (asset) => {
        setSelectedAsset(asset);
        setTransactionType("SELL");
        setQuantity(1);
        setShowModal(true);
    };

    return (
        <div className="container-fluid p-4">
            <div className="row">
                <div className="col-md-4"><AssetSection title="Cryptocurrencies" assets={data.cryptocurrencies} /></div>
                <div className="col-md-4"><AssetSection title="Stocks" assets={data.stocks} /></div>
                <div className="col-md-4"><AssetSection title="ETFs" assets={data.etfs} /></div>
            </div>

            <Modal show={showModal} onHide={() => setShowModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Buy {selectedAsset?.symbol}</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group>
                            <Form.Label>Quantity</Form.Label>
                            <Form.Control type="number" min="1" value={quantity} onChange={(e) => setQuantity(e.target.value)} />
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowModal(false)}>Cancel</Button>
                    <Button variant={transactionType === "BUY" ? "primary" : "danger"} onClick={() => handlePurchase(transactionType)}>
                        Confirm {transactionType === "BUY" ? "Purchase" : "Sale"}
                    </Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
};

export default MarketData;
