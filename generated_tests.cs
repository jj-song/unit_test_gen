using NUnit.Framework;

[TestFixture]
public class RectangleTests
{
    [Test]
    public void CalculateArea_WithPositiveLengthAndWidth_ReturnsCorrectArea()
    {
        // Arrange
        double length = 5;
        double width = 3;
        double expectedArea = 15;
        Rectangle rectangle = new Rectangle(length, width);
        
        // Act
        double actualArea = rectangle.CalculateArea();
        
        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
    
    [Test]
    public void CalculateArea_WithZeroLength_ReturnsZeroArea()
    {
        // Arrange
        double length = 0;
        double width = 3;
        double expectedArea = 0;
        Rectangle rectangle = new Rectangle(length, width);
        
        // Act
        double actualArea = rectangle.CalculateArea();
        
        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
    
    [Test]
    public void CalculateArea_WithZeroWidth_ReturnsZeroArea()
    {
        // Arrange
        double length = 5;
        double width = 0;
        double expectedArea = 0;
        Rectangle rectangle = new Rectangle(length, width);
        
        // Act
        double actualArea = rectangle.CalculateArea();
        
        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
    
    [Test]
    public void CalculateArea_WithNegativeLength_ReturnsNegativeArea()
    {
        // Arrange
        double length = -5;
        double width = 3;
        double expectedArea = -15;
        Rectangle rectangle = new Rectangle(length, width);
        
        // Act
        double actualArea = rectangle.CalculateArea();
        
        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
    
    [Test]
    public void CalculateArea_WithNegativeWidth_ReturnsNegativeArea()
    {
        // Arrange
        double length = 5;
        double width = -3;
        double expectedArea = -15;
        Rectangle rectangle = new Rectangle(length, width);
        
        // Act
        double actualArea = rectangle.CalculateArea();
        
        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
}