using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;
using Teacher_Centric_Platform.Application.Common.Interfaces;
using Teacher_Centric_Platform.Domain.Common;
using Teacher_Centric_Platform.Infrastructure.Identity;
using Teacher_Centric_Platform.Domain.Entities.Course;
using Teacher_Centric_Platform.Domain.Entities.QA;
using Teacher_Centric_Platform.Domain.Entities.Exam;
using Teacher_Centric_Platform.Domain.Entities.Assignment;
namespace Teacher_Centric_Platform.Infrastructure.Persistence
{
    public class Teacher_CentricDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    { 
        private readonly ICurrentUserService _currentUserService;

        public Teacher_CentricDbContext(
            DbContextOptions<Teacher_CentricDbContext> options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }



        public DbSet<Course> Courses => Set<Course>();

        public DbSet<QAThread> QAThreads => Set<QAThread>();

        public DbSet<QAPost> QAPosts => Set<QAPost>();

        public DbSet<Exam> Exams => Set<Exam>();
        public DbSet<Assignment> Assignments => Set<Assignment>();
        public DbSet<AssignmentSubmission> AssignmentSubmissions => Set<AssignmentSubmission>();

        public DbSet<SubmissionAnswer> SubmissionAnswers => Set<SubmissionAnswer>();

        public DbSet<ExamAttempt> ExamQuestions => Set<ExamAttempt>();

        public DbSet<ExamAttemptAnswer> ExamAttemptAnswers => Set<ExamAttemptAnswer>();

        public DbSet<Question> Questions => Set<Question>();

        public DbSet<QuestionOption> QuestionOptions => Set<QuestionOption>();

        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        public DbSet<Module> Modules => Set<Module>();

        public DbSet<Lesson> Lessons => Set<Lesson>();







        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Teacher_CentricDbContext).Assembly);

            // Global query filter: automatically exclude soft-deleted entities
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = System.Linq.Expressions.Expression.Parameter(entityType.ClrType, "e");
                    var property = System.Linq.Expressions.Expression.Property(parameter, nameof(BaseEntity.IsDeleted));
                    var falseConstant = System.Linq.Expressions.Expression.Constant(false);
                    var condition = System.Linq.Expressions.Expression.Equal(property, falseConstant);
                    var lambda = System.Linq.Expressions.Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentTime = DateTime.UtcNow;
            var userId = _currentUserService.UserId;

            ApplySoftDelete(currentTime, userId);

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplySoftDelete(DateTime now, string? userId)
        {
            var deletedEntries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Modified; 
                entry.Entity.IsDeleted = true;
            }
        }

     
    }
}
