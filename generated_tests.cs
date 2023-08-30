using Xunit;

public class RectangleTests
{
    [Fact]
    public void CalculateArea_ReturnsCorrectArea()
    {
        // Arrange
        double length = 4;
        double width = 5;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(20, area);
    }

    [Fact]
    public void CalculateArea_ReturnsZeroWhenLengthIsZero()
    {
        // Arrange
        double length = 0;
        double width = 5;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(0, area);
    }

    [Fact]
    public void CalculateArea_ReturnsZeroWhenWidthIsZero()
    {
        // Arrange
        double length = 4;
        double width = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(0, area);
    }

    [Fact]
    public void CalculateArea_ReturnsZeroWhenBothLengthAndWidthAreZero()
    {
        // Arrange
        double length = 0;
        double width = 0;
        Rectangle rectangle = new Rectangle(length, width);

        // Act
        double area = rectangle.CalculateArea();

        // Assert
        Assert.Equal(0, area);
    }
}