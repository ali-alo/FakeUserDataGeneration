# Fake User Data Generator Web Application

This web application allows you to generate fake (random) user data based on a selected region. It provides various options to customize the data generation process, including specifying the number of errors per record, defining a seed value, and supporting multiple regions. For quick access, go to the deployed application at https://fake-user-data-generator-ali-alo.azurewebsites.net/
## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Optional Export to CSV](#optional-export-to-csv)
- [Data Generation Details](#data-generation-details)

## Features

1. **Region Selection**: Choose from three different regions (USA, France, Russia) to generate region-specific user data.

2. **Error Generation**: Specify the number of errors per record using a slider (0 to 10) or a numeric field (with a maximum value limit of at least 1000). Errors are randomly applied to simulate data entry errors.

3. **Seed Value Customization**: Define a seed value and use the [Random] button to generate a random seed. Changing the seed value regenerates the data.

4. **Infinite Scrolling**: The table displays 20 records initially. When you scroll down, an additional 10 records are added.

5. **Realistic Data**: The generated data includes realistic information such as names, addresses, and phone numbers, with language and region correlation.

## Technologies

- .NET 6.0
- Blazor WebAssembly
- [Bogus](https://github.com/bchavez/Bogus) for fake data generation
- [CsvHelper](https://joshclose.github.io/CsvHelper/) for CSV export

### CI/CD with GitHub Actions and Azure

This project implements Continuous Integration and Continuous Deployment (CI/CD) using GitHub Actions and Azure. It ensures a streamlined development and deployment process:

- **GitHub Actions**: GitHub Actions automates tasks such as building, artifact generation, and deployment.

- **Azure**: Azure hosts and deploys the application, providing scalability and reliability.


## Getting Started

To get started with this project, follow these steps:

1. Clone the repository to your local machine:

   ```shell
   git clone https://github.com/ali-alo/FakeUserDataGeneration.git
   ```

2. Navigate to the project folder:

   ```shell
   cd FakeUserDataGeneration
   ```

3. Install the required dependencies by running:

    ```shell
    dotnet restore
    ```

4. Build and run the application:

   ```shell
   dotnet run
   ```

5. Access the application in your web browser at https://localhost:7010

## Usage

1. Select a region from the available options.

2. Define a seed value or generate a random seed using the [Random] button.

3. Use the slider or numeric field to specify the number of errors per record.

4. Any change to the region, or seed value will regenerate the data table with the specified settings.

5. Changes to the error count will only apply errors to the existing data.

5. Scroll down to view additional records as they are loaded in batches.

### Optional Export to CSV

You can export the currently displayed data to CSV by clicking the "Download CSV" button. This will generate a CSV file with the current data, ready for download.

## Data Generation Details

- Data generated is based on lookup tables for names, surnames, cities, etc., ensuring realistic data without full duplication.

- Errors are applied according to the specified error count (per record), including character deletions, additions, and character swaps.

- The seed value is used to ensure reproducibility of generated data. The combination of the user seed and page number prevents unnecessary regeneration. IFor instance, seed 777 will always produce the same fake data.
