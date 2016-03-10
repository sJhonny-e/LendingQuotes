using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LendingQuotes.Model;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using LendingQuotes.Model.Interfaces;

namespace LendingQuotes.DAL
{
    public class CsvLendersRepository : ILendersRepository
    {
        private List<Lender> allLenders;
        public CsvLendersRepository(string csvFileName)
        {
            // TODO: handle possible errors here (format / file not found / file not readable, etc..)
            using (var reader = new StreamReader(csvFileName))
            {
                var config = new CsvConfiguration();
                config.RegisterClassMap<LenderClassMap>();
                var csvReader = new CsvReader(reader, config);

                allLenders = csvReader.GetRecords<Lender>().ToList();
            }
        }

        public IEnumerable<Lender> GetAllLenders()
        {
            return allLenders;
        }

        // Mapping class, used with CsvHelper
        private class LenderClassMap : CsvClassMap<Lender>
        {
            public LenderClassMap()
            {
                Map(l => l.Name).Name("Lender");
                Map(l => l.Rate);
                Map(l => l.Amount).Name("Available");
            }
        }
    }
}
