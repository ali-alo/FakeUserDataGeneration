using Bogus;
using FakeUserDataGeneration.Models;

namespace FakeUserDataGeneration.Services
{
    public class ErrorGenerationService
    {
        private Faker<Error> _faker = new();
        private Faker<ErrorProbability> _fakerErrorProbability = new();
        private Faker<ErrorInputs> _fakerErrorInputs = new();

        public void ApplyErrors(List<FakeUser> users, float errorAmount, string locale, int seed)
        {
            CreateFakers(locale, seed);
            foreach (var user in users)
            {
                int errorsToMake = RoundNumber(errorAmount);
                var errors = _faker.Generate(errorsToMake);
                foreach (var error in errors)
                {
                    var errorInputs = _fakerErrorInputs.Generate();
                    if (error.ErrorOnProperty == ErrorOnProperty.FullName)
                        GenerateError(user, error, errorInputs, user.FullName);
                    else if (error.ErrorOnProperty == ErrorOnProperty.Address)
                        GenerateError(user, error, errorInputs, user.Address);
                    else
                        GenerateError(user, error, errorInputs, user.Phone);
                }
            }
        }

        private int RoundNumber(float errorAmount)
        {
            float fraction = errorAmount % 1;
            float randomValue = _fakerErrorProbability.Generate().Probability;
            return randomValue <= fraction ? (int)Math.Ceiling(errorAmount) : (int)Math.Floor(errorAmount);
        }

        private void GenerateError(FakeUser user, Error error, ErrorInputs errorInputs, string property)
        {
            _fakerErrorInputs.RuleFor(x => x.Index, x => x.Random.Int(min: 0, max: property.Length - 2));
            errorInputs.Index = _fakerErrorInputs.Generate().Index;
            if (property.Length < 2)
                error.Type = ErrorType.Insert;
            else if (property.Length > 70)
                error.Type = ErrorType.Remove;
            property = MakeError(error, errorInputs, property);
            UpdateUserProperty(user, error,  property);
        }

        private void UpdateUserProperty(FakeUser user, Error error, string property)
        {
            if (error.ErrorOnProperty == ErrorOnProperty.FullName)
                user.FullName = property;
            else if (error.ErrorOnProperty == ErrorOnProperty.Address)
                user.Address = property;
            else
                user.Phone = property;
        }

        private string MakeError(Error error, ErrorInputs errorInputs, string property)
        {
            if (error.Type == ErrorType.Insert)
                property = property.Insert(errorInputs.Index, error.ErrorOnProperty == ErrorOnProperty.Phone ?
                                                        errorInputs.DigitToInsert.ToString() : errorInputs.LetterToInsert);
            else if (error.Type == ErrorType.Swap)
                property = SwapTwoAdjacentChars(property, errorInputs.Index);
            else
                property = property.Remove(errorInputs.Index, 1);
            return property;
        }

        private string SwapTwoAdjacentChars(string property, int index)
        {
            char[] charArray = property.ToCharArray();
            char temp = charArray[index];
            charArray[index] = charArray[index + 1];
            charArray[index + 1] = temp;
            return new string(charArray);
        }

        private void CreateFakers(string locale, int seed)
        {
            Randomizer.Seed = new Random(seed);
            _fakerErrorProbability = new Faker<ErrorProbability>()
                    .RuleFor(x => x.Probability, x => x.Random.Float());
            _faker = new Faker<Error>(locale)
                    .RuleFor(x => x.ErrorOnProperty, x => (ErrorOnProperty)x.Random.Int(min: 0, max: 2))
                    .RuleFor(x => x.Type, x => (ErrorType)x.Random.Int(min: 0, max: 2));
            _fakerErrorInputs = new Faker<ErrorInputs>(locale)
                    .RuleFor(x => x.DigitToInsert, x => x.Random.Int(min: 0, max: 9))
                    .RuleFor(x => x.LetterToInsert, x => x.Lorem.Letter(num: 1));
        }
    }


}
