using NUnit.Framework;

[TestFixture]
public class RectangleTests
{
    [Test]
    public void CalculateArea_LengthAndWidthArePositive_ReturnsCorrectArea()
    {
        // Arrange
        double length = 5;
        double width = 4;
        double expectedArea = 20;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [Test]
    public void CalculateArea_LengthIsNegative_ReturnsZero()
    {
        // Arrange
        double length = -5;
        double width = 4;
        double expectedArea = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [Test]
    public void CalculateArea_WidthIsNegative_ReturnsZero()
    {
        // Arrange
        double length = 5;
        double width = -4;
        double expectedArea = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [Test]
    public void CalculateArea_LengthAndWidthAreZero_ReturnsZero()
    {
        // Arrange
        double length = 0;
        double width = 0;
        double expectedArea = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }
}