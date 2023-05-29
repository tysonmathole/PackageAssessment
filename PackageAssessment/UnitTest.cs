using NUnit.Framework;

[TestFixture]
public class PackageOptimizerTests
{
    [Test]
    public void ParseItems_ValidItemsString_ReturnsListOfItems()
    {
        // Arrange
        string itemsStr = "(1, 2.5, $10) (2, 3.7, $15) (3, 1.8, $8)";

        // Act
        List<PackageOptimizer.Item> items = PackageOptimizer.ParseItems(itemsStr);

        // Assert
        Assert.AreEqual(3, items.Count);

        Assert.AreEqual(1, items[0].Index);
        Assert.AreEqual(2.5, items[0].Weight);
        Assert.AreEqual(10, items[0].Cost);

        Assert.AreEqual(2, items[1].Index);
        Assert.AreEqual(3.7, items[1].Weight);
        Assert.AreEqual(15, items[1].Cost);

        Assert.AreEqual(3, items[2].Index);
        Assert.AreEqual(1.8, items[2].Weight);
        Assert.AreEqual(8, items[2].Cost);
    }

    /* [Test]
     public void OptimizePackage_ReturnsOptimalSelection()
     {
         // Arrange
         int weightLimit = 10;
         List<PackageOptimizer.Item> items = new List<PackageOptimizer.Item>()
         {

             new Item { Index = 1, Weight = 2, Cost = 3 },
             new Item { Index = 2, Weight = 4, Cost = 5 },
             new Item { Index = 3, Weight = 5, Cost = 8 },
             new Item { Index = 4, Weight = 3, Cost = 6 }
         };

         // Act
         List<int> selectedItems = PackageOptimizer.OptimizePackage(weightLimit, items);

         // Assert
         CollectionAssert.AreEqual(new List<int> { 1, 4 }, selectedItems);
}*/
}