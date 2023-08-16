namespace FakeUserDataGeneration.Models
{
    public class Error
    {
        public ErrorType Type { get; set; }
        public ErrorOnProperty ErrorOnProperty { get; set; }
        public ErrorInputs Inputs { get; set; }
    }
}
