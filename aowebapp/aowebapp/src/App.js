import './App.css';
import Card from './components/Card'; 
import CardV2 from './components/CardV2';
import CardV3 from './components/CardV3';
function App() {
  return (
      <div className="App container">
          <div className="bg-light py-1 mb-2">
              <h2 className="text-center">EXAMPLE TEXT</h2>
          </div>
          <div className="row justify-content-center">
              {/* Cards go here */}
              <Card itemId="1"
                  itemName="test record1"
                  itemDescription="this is a test record for 1 description"
                  itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                  itemCost="15.00"
              />
              <CardV2 itemId="2"
                  itemName="test record2"
                  itemDescription="this is a test record for 2 description"
                  itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                  itemCost="10.00"
              />
              <CardV3 itemId="5"
                  itemName="test record3"
                  itemDescription="this is a test record for 3 description"
                  itemImage="https://upload.wikimedia.org/wikipedia/commons/0/04/So_happy_smiling_cat.jpg"
                  itemCost="5.00"
              />
            
          </div>
      </div>
  );
}

export default App;
