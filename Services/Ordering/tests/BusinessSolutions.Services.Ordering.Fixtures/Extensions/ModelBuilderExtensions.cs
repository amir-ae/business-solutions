using Newtonsoft.Json;

namespace BusinessSolutions.Services.Ordering.Fixtures.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder Seed<T>(this ModelBuilder modelBuilder, string file) where T : class
        {
            using (var reader = new StreamReader(file))
            {
                var json = reader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<T[]>(json);
                modelBuilder.Entity<T>().HasData(data!);
            }
            return modelBuilder;
        }
    }
}
