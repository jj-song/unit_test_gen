public class RectangleTests
  {
    [Fact]
    public void Rectangle_Area_ShouldBeCalculatedCorrectly()
    {
      //Arrange
      double length = 5d;
      double width = 3d;
      var rectangle = new Rectangle(length, width);

      //Act
      double area = rectangle.CalculateArea();

      //Assert
      double expectedArea = length * width;
      Assert.Equal(expectedArea, area);
    }
  }