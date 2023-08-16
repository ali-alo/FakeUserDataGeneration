using CsvHelper.Configuration;
using CsvHelper;
using FakeUserDataGeneration.Models;
using Microsoft.AspNetCore.Components;
using System.Globalization;
using System.Text;

namespace FakeUserDataGeneration.Components
{
    public partial class CsvButton
    {
        [Parameter]
        public IEnumerable<FakeUser> Users { get; set; }

        private string GenerateCsv()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true
            };

            var csv = new StringBuilder();
            using (var csvWriter = new CsvWriter(new StringWriter(csv), csvConfig))
            {
                csvWriter.WriteRecords(Users);
            }

            return csv.ToString();
        }
    }
}
