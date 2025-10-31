import { Link, Outlet } from "react-router-dom";
import Card from './components/Card';
import CardListSearch from './components/CardList';
import CardV2 from './components/CardV2';
import CardV3 from './components/CardV3';
function App() {
    return (
        <div className="App container">
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
                <div className="container-fluid">
                    <Link className="navbar-brand" to="/">AOWebApp</Link>
                    <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
                        <div className="navbar-nav">
                            <Link className="nav-link active" to="/Home">Home</Link>
                            <Link className="nav-link" to="/Contact">Contact</Link>
                            <Link className="nav-link" to="/Products">Products</Link>
                        </div>
                    </div>
                </div>
            </nav>
            <Outlet />

            {/* wk11 content */}
            {/*<div className="bg-light py-1 mb-2">*/}
            {/*    <h2 className="text-center">EXAMPLE TEXT</h2>*/}
            {/*</div>*/}
            {/*<div className="row justify-content-center">*/}
            {/*    */}{/* Cards go here */}
            {/*    <Card itemId="1"*/}
            {/*        itemName="test record1"*/}
            {/*        itemDescription="this is a test record for 1 description"*/}
            {/*        itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"*/}
            {/*        itemCost="15.00"*/}
            {/*    />*/}
            {/*    <CardV2 itemId="2"*/}
            {/*        itemName="test record2"*/}
            {/*        itemDescription="this is a test record for 2 description"*/}
            {/*        itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"*/}
            {/*        itemCost="10.00"*/}
            {/*    />*/}
            {/*    <CardV3 itemId="5"*/}
            {/*        itemName="test record3"*/}
            {/*        itemDescription="this is a test record for 3 description"*/}
            {/*        itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"*/}
            {/*        itemCost="5.00"*/}
            {/*    />*/}
            {/*    <CardListSearch />*/}
            {/*</div>*/}
        </div>
    );
}

export default App;