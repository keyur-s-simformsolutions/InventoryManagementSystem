using System;
using System.Linq;
using InventoryManagementSystem.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;



namespace FunctionApp1
{
    public class Function1
    {
        private readonly ApplicationDbContext _appDBContext;

        public Function1(ApplicationDbContext appDBContext)
        {
            _appDBContext = appDBContext;
        }



        [FunctionName("Function1")]
        public void Run([TimerTrigger("* * * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                var todaysItem = _appDBContext.Item.ToList().Where(x => x.DateofAddingItem.Date == DateTime.Now.Date);
                foreach (var item in todaysItem)
                {
                    log.LogInformation($"Item Name: {item.Name}, Today's Date: {item.DateofAddingItem}, Quantity: {item.StockItem}. The number of new items added today is successful..");
                }
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");



            }
            catch (Exception e)
            {
                log.LogInformation($"C# Timer trigger function executed but have error at: {DateTime.Now} : " +
                  $"error Message : {e.Message} : error StackTrace : {e.StackTrace}");
                throw e;
            }




        }
    }
}