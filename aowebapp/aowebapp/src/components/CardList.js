import Card from "./CardV3"
import cardData from "../assets/itemData.json"

const CardList = ({ }) => {
    //let cardData = [
    //    { itemId: 1, itemName: "record 1", itemDescription: "record
    //    { itemId: 2, itemName: "record 2", itemDescription: "record
    //    { itemId: 3, itemName: "record 3", itemDescription: "record
    //]

    console.log("cardData: " + cardData);
    return (
        <div className="row">
            {cardData.map((obj) => (
                <Card
                    key={obj.itemId}
                    itemId={obj.itemId}
                    itemName={obj.itemName}
                    itemDescription={obj.itemDescription}
                    itemCost={obj.itemCost}
                    itemImage={obj.itemImage}
                />
            ))}
        </div>
    )
}

export default CardList