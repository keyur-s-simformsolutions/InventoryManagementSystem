using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InventoryManagementSystem.Data
{
    public class ReportService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ReportService> _logger;

        public ReportService(ApplicationDbContext dbContext, ILogger<ReportService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<byte[]> GenerateInwardReportAsync()
        {
            try
            {
                List<Item> items = await _dbContext.Item.ToListAsync();

                // Generate the report using the items data
                StringBuilder reportBuilder = new StringBuilder();
                reportBuilder.AppendLine("Item Name, Category, Description, Price, Stock Item, Date of Adding Item");

                foreach (Item item in items)
                {
                    reportBuilder.AppendLine($"{item.Name}, {item.Category}, {item.Description}, {item.Price}, {item.StockItem}, {item.DateofAddingItem}");
                }
                _logger.LogInformation("Inward report generated successfully");
                // Convert the report to a byte array
                byte[] reportBytes = Encoding.UTF8.GetBytes(reportBuilder.ToString());

                return reportBytes;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the report generation
                _logger.LogError(ex, "Error generating inward report");
                throw;
            }
        }

        public async Task<bool> DownloadInwardReportAsync(string filePath)
        {
            try
            {
                byte[] reportBytes = await GenerateInwardReportAsync();

                if (reportBytes == null)
                    return false;

                // Save the report to the specified file path
                await File.WriteAllBytesAsync(filePath, reportBytes);

                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the download process
                // Log the exception or perform any necessary error handling
                Console.WriteLine($"Error downloading inward report: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DownloadMultipleInwardReportsAsync(string directoryPath, int count)
        {
            try
            {
                if (count <= 0)
                    return false;

                for (int i = 1; i <= count; i++)
                {
                    string fileName = $"InwardReport{i}.csv";
                    string filePath = Path.Combine(directoryPath, fileName);

                    bool result = await DownloadInwardReportAsync(filePath);

                    if (!result)
                    {
                        Console.WriteLine($"Error downloading inward report {i}");
                        // You can choose to handle the error or continue with the remaining reports
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the download process
                // Log the exception or perform any necessary error handling
                Console.WriteLine($"Error downloading multiple inward reports: {ex.Message}");
                return false;
            }
        }
    }
}
