namespace BoxCutting.Core.Models
{
    public class Sheet
    {
        public int Width { get; set; }
        public int Length { get; set; }

        public int Area => Width * Length;

        public Sheet(int width, int length)
        {
            Width = width;
            Length = length;
        }
    }
}