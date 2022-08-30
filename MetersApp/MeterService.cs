using MetersApp.Data;
using MetersApp.Models;
using MetersApp.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace MetersApp
{
    public class MeterService
    {
        private readonly DataContext _dbContext;

        public MeterService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IndicationPanelModel> GetCurrentResources()
        {
            var meters = _dbContext.Meter.AsNoTracking().Include(m => m.Resource).ToList();

            var indications = meters.Select(m =>
            {
                var values = _dbContext.Indication.AsNoTracking()
                    .Where(i => i.MeterId == m.Id)
                    .OrderByDescending(i => i.Fixed)
                    .Take(2).ToList();

                return new MeterIndicationServiceModel
                {
                    ResourceId = m.ResourceId,
                    ResourceTitle = m.Resource.Title,
                    Date = values.Count == 0 ? DateOnly.MaxValue : DateOnly.FromDateTime(values[0].Fixed),
                    Delta = values.Count == 0 ? 0 : (values.Count == 1 ? values[0].Data : values[0].Data - values[1].Data),
                    Value = values.Count == 0 ? null : values[0].Data
                };

            }).Where(i => i.Value != null).ToList();

            return indications.GroupBy(i => i.ResourceId).Select(g => new IndicationPanelModel
            {
                Id = g.First().ResourceId,
                Title = g.First().ResourceTitle,
                Value = g.Sum(i => i.Value ?? 0),
                Delta = g.Sum(i => i.Delta),
                Date = g.Max(i => i.Date)
            }).ToList();
        }

        public List<MeterPanelsModel> GetCurrentResourceIndications(int id)
        {
            var meters = _dbContext.Meter.AsNoTracking().Include(m => m.Resource).Where(i => i.ResourceId == id).ToList();

            var indications = meters.Select(m =>
            {
                var values = _dbContext.Indication.AsNoTracking()
                    .Where(i => i.MeterId == m.Id)
                    .OrderByDescending(i => i.Fixed)
                    .Take(2).ToList();

                return new IndicationMeterServiceModel
                {
                    MeterId = m.Id,
                    MeterTitle = m.Title,
                    Date = values.Count == 0 ? DateOnly.MaxValue : DateOnly.FromDateTime(values[0].Fixed),
                    Delta = values.Count == 0 ? 0 : (values.Count == 1 ? values[0].Data : values[0].Data - values[1].Data),
                    Value = values.Count == 0 ? null : values[0].Data
                };

            }).Where(i => i.Value != null).ToList();

            return indications.Select(m => new MeterPanelsModel
            {
                Id = m.MeterId,
                Title = m.MeterTitle,
                Value = m.Value ?? 0,
                Delta = m.Delta,
                Date = m.Date
            }).ToList();
        }

        public List<CounterModel> GetCounters()
        {
            var counters = _dbContext.Meter.AsNoTracking().ToList();

            return counters.Select(g => new CounterModel
            {
                Id = g.Id,
                Title = g.Title,
            }).ToList();
        }

        public void SaveData(NewIndication model)
        {
            var indications = new Indication()
            {
                MeterId = model.MeterId,
                Fixed = DateTime.Now,
                Data = model.Indication
            };

            _dbContext.Indication.Add(indications);
            _dbContext.SaveChanges();
        }

        public List<IndicationHistoryModel> GetHistoryOfIndications(int id)
        {
            var indications = _dbContext.Indication.AsNoTracking().Where(i => i.MeterId == id).ToList();

            var history = indications.Select(m => 
            {
                var meters = _dbContext.Meter.AsNoTracking().Where(i => i.Id == id).ToList();

                return new HistoryModel
                {
                    MeterTitle = meters[0].Title,
                    MeterId = meters[0].Id,
                    Fixed = DateOnly.FromDateTime(m.Fixed),
                    Data = m.Data
                };
            }).ToList();

            return history.Select(g => new IndicationHistoryModel
            {
                Title = g.MeterTitle,
                Fixed = g.Fixed,
                Data = g.Data
            }).ToList();
        }
    }
}