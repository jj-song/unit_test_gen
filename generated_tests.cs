Here are some NUnit unit tests to maximize code coverage for the Rectangle class:

[TestFixture]
public class RectangleTests
{
    [Test]
    public void CalculateArea_ReturnsCorrectArea()
    {
        // Arrange
        double length = 5;
        double width = 10;
        double expectedArea = length * width;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [Test]
    public void CalculateArea_ReturnsZero_WhenLengthIsZero()
    {
        // Arrange
        double length = 0;
        double width = 10;
        double expectedArea = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double actualArea = rectangle.CalculateArea();

        // Assert
        Assert.AreEqual(expectedArea, actualArea);
    }

    [Test]
    public void CalculateArea_ReturnsZero_WhenWidthIsZero()
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
    public void CalculateArea_ReturnsZero_WhenLengthAndWidthAreZero()
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