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
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double result = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(20, result);
    }

    [Test]
    public void CalculateArea_LengthIsZero_ReturnsZeroArea()
    {
        // Arrange
        double length = 0;
        double width = 4;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double result = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(0, result);
    }

    [Test]
    public void CalculateArea_WidthIsZero_ReturnsZeroArea()
    {
        // Arrange
        double length = 5;
        double width = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double result = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(0, result);
    }

    [Test]
    public void CalculateArea_LengthAndWidthAreNegative_ReturnsCorrectArea()
    {
        // Arrange
        double length = -5;
        double width = -4;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double result = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(20, result);
    }
}