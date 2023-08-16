using FakeUserDataGeneration.Models;
using FakeUserDataGeneration.Services;
using Microsoft.AspNetCore.Components;
using System.Globalization;

namespace FakeUserDataGeneration.Pages
{
    public partial class Index
    {
        private int page = 1;
        private string locale = string.Empty;
        private int? seed;
        private float errorNumber = 0.0f;
        private int errorSlider = 0;

        public List<FakeUser> FakeUsers { get; set; } = new List<FakeUser>();

        [Inject]
        public UsersGenerationService UsersGenService { get; set; }

        [Inject]
        public ErrorGenerationService ErrorGenService { get; set; }

        public void  FetchTenUsers(bool applyErrors = false)
        {
            var newUsers = UsersGenService.GenerateTenUsers();
            if (errorNumber != 0 && applyErrors)
                ApplyErrorsOnNewUsers(newUsers);
            FakeUsers.AddRange(newUsers);
            page++;
            UpdateFaker();
        }


        private void ChangeRegion(string region)
        {
            locale = region;
            TryResetUsersList();
        }

        private void TryResetUsersList()
        {
            if (!string.IsNullOrEmpty(locale) && seed != null)
            {
                errorNumber = 0.0f;
                errorSlider = 0;
                ResetUsersList();
            }
        }

        private void ApplyErrors()
        {
            // remove all errors first
            GetCleanData();
            ErrorGenService.ApplyErrors(FakeUsers, errorNumber, locale, page);
        }

        private void ApplyErrorsOnNewUsers(List<FakeUser> users)
        {
            ErrorGenService.ApplyErrors(users, errorNumber, locale, page);
        }

        private void GetCleanData()
        {
            int length = FakeUsers.Count;
            ResetUsersList();
            UpdateFaker();
            for (int i = FakeUsers.Count; i < length; i += 10)
                FetchTenUsers(true);
        }
        private void ChangeSeed(ChangeEventArgs args)
        {
            if (int.TryParse(args?.Value?.ToString(), NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out _))
            {
                seed = Convert.ToInt32(args.Value);
                TryResetUsersList();
            }
        }

        private void GenerateRandomSeed()
        {
            seed = new Random().Next(0, 100_000);
            TryResetUsersList();
        }

        private void ResetUsersList()
        {
            FakeUsers.Clear();
            page = 1;
            UpdateFaker();
            FetchTenUsers();
            FetchTenUsers();
        }

        private void UpdateFaker() => UsersGenService.UpdateFakerLocale(locale, page*seed!.Value);

        private void UpdateErrorNumber(ChangeEventArgs e)
        {
            if (int.TryParse(e?.Value?.ToString(), out errorSlider))
            {
                float convertedValue = errorSlider / 4.0f;
                if (errorNumber != convertedValue)
                    errorNumber = convertedValue;
                if (FakeUsers.Count > 0)
                    ApplyErrors();
            }
        }

        private void UpdateErrorSlider(ChangeEventArgs e)
        {
            if (float.TryParse((string)e.Value!, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out errorNumber))
            {
                if (errorNumber > 1000)
                    errorNumber = 0;
                if (errorSlider != errorNumber * 4)
                    errorSlider = Math.Min((int)(errorNumber * 4), 40);
                if (FakeUsers.Count > 0)
                    ApplyErrors();
            }
        }
    }
}
