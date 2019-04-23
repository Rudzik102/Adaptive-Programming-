using System.Data.Entity;

namespace ProjectTPA.Database
{
    public class DBaseContext : DbContext
    {
        public DBaseContext(string connectionString) : base(connectionString) { }

        public virtual DbSet<DbAssembly> Assembly { get; set; }
        public virtual DbSet<DbMethod> Method { get; set; }
        public virtual DbSet<DbNamespace> Namespace { get; set; }
        public virtual DbSet<DbParameter> Parameter { get; set; }
        public virtual DbSet<DbProperty> Property { get; set; }
        public virtual DbSet<DbType> Type { get; set; }

        public virtual DbSet<DbLog> Log { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}
