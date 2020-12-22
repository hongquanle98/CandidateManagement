using System;
using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CandidateManagement.Models
{
    public partial class CandidateManagementContext : IdentityDbContext
    {
        public CandidateManagementContext()
        {
        }

        public CandidateManagementContext(DbContextOptions<CandidateManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ability> Ability { get; set; }
        public virtual DbSet<Apply> Apply { get; set; }
        public virtual DbSet<ApplyDetail> ApplyDetail { get; set; }
        public virtual DbSet<ApplyDetailAbility> ApplyDetailAbility { get; set; }
        public virtual DbSet<ApplyPosition> ApplyPosition { get; set; }
        public virtual DbSet<ApplyPositionAbility> ApplyPositionAbility { get; set; }
        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<CandidateBackground> CandidateBackground { get; set; }
        public virtual DbSet<CandidateCertificate> CandidateCertificate { get; set; }
        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<College> College { get; set; }
        public virtual DbSet<InterviewResult> InterviewResult { get; set; }
        public virtual DbSet<InterviewSchedule> InterviewSchedule { get; set; }
        public virtual DbSet<Operator> Operator { get; set; }
        public virtual DbSet<RequiredAbility> RequiredAbility { get; set; }
        public virtual DbSet<RequiredAbilityPoint> RequiredAbilityPoint { get; set; }
        public virtual DbSet<Requirement> Requirement { get; set; }
        public virtual DbSet<RequirementCertificate> RequirementCertificate { get; set; }
        public virtual DbSet<SavedRequirement> SavedRequirement { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=QUAN-PC;Database=CandidateManagement;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ability>(entity =>
            {
                entity.Property(e => e.AbilityId).HasColumnName("AbilityID");

                entity.Property(e => e.AbilityName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Note).HasMaxLength(150);
            });

            modelBuilder.Entity<Apply>(entity =>
            {
                entity.Property(e => e.ApplyId).HasColumnName("ApplyID");

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Apply)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Apply_Candidate");
            });

            modelBuilder.Entity<ApplyDetail>(entity =>
            {
                entity.Property(e => e.ApplyDetailId).HasColumnName("ApplyDetailID");

                entity.Property(e => e.ApplyDate).HasColumnType("date");

                entity.Property(e => e.ApplyId).HasColumnName("ApplyID");

                entity.Property(e => e.CvfilePath)
                    .HasColumnName("CVFilePath")
                    .HasMaxLength(150);

                entity.Property(e => e.ExpectedSalary).HasColumnType("money");

                entity.Property(e => e.IsCvpass).HasColumnName("IsCVPass");

                entity.Property(e => e.RequirementId).HasColumnName("RequirementID");

                entity.HasOne(d => d.Apply)
                    .WithMany(p => p.ApplyDetail)
                    .HasForeignKey(d => d.ApplyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyDetail_Apply");

                entity.HasOne(d => d.Requirement)
                    .WithMany(p => p.ApplyDetail)
                    .HasForeignKey(d => d.RequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyDetail_Requirement");
            });

            modelBuilder.Entity<ApplyDetailAbility>(entity =>
            {
                entity.Property(e => e.ApplyDetailAbilityId).HasColumnName("ApplyDetailAbilityID");

                entity.Property(e => e.AbilityId).HasColumnName("AbilityID");

                entity.Property(e => e.ApplyDetailId).HasColumnName("ApplyDetailID");

                entity.HasOne(d => d.Ability)
                    .WithMany(p => p.ApplyDetailAbility)
                    .HasForeignKey(d => d.AbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyDetailAbility_Ability");

                entity.HasOne(d => d.ApplyDetail)
                    .WithMany(p => p.ApplyDetailAbility)
                    .HasForeignKey(d => d.ApplyDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyDetailAbility_ApplyDetail");
            });

            modelBuilder.Entity<ApplyPosition>(entity =>
            {
                entity.Property(e => e.ApplyPositionId).HasColumnName("ApplyPositionID");

                entity.Property(e => e.ApplyPositionName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ApplyPositionAbility>(entity =>
            {
                entity.HasKey(e => new { e.ApplyPositionId, e.AbilityId });

                entity.Property(e => e.ApplyPositionId).HasColumnName("ApplyPositionID");

                entity.Property(e => e.AbilityId).HasColumnName("AbilityID");

                entity.HasOne(d => d.Ability)
                    .WithMany(p => p.ApplyPositionAbility)
                    .HasForeignKey(d => d.AbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyPositionAbility_Ability");

                entity.HasOne(d => d.ApplyPosition)
                    .WithMany(p => p.ApplyPositionAbility)
                    .HasForeignKey(d => d.ApplyPositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplyPositionAbility_ApplyPosition");
            });

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("Unique_Candidate_UserID")
                    .IsUnique();

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.AvatarPath).HasMaxLength(200);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(1);

                entity.Property(e => e.IdentityNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Telephone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<CandidateBackground>(entity =>
            {
                entity.HasKey(e => e.CandidateId);

                entity.Property(e => e.CandidateId)
                    .HasColumnName("CandidateID")
                    .ValueGeneratedNever();

                entity.Property(e => e.CollegeId).HasColumnName("CollegeID");

                entity.Property(e => e.CurrentGpa).HasColumnName("CurrentGPA");

                entity.Property(e => e.GraduateDate).HasColumnType("date");

                entity.Property(e => e.Major).HasMaxLength(50);

                entity.HasOne(d => d.Candidate)
                    .WithOne(p => p.CandidateBackground)
                    .HasForeignKey<CandidateBackground>(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateBackground_Candidate");

                entity.HasOne(d => d.College)
                    .WithMany(p => p.CandidateBackground)
                    .HasForeignKey(d => d.CollegeId)
                    .HasConstraintName("FK_CandidateBackground_College");
            });

            modelBuilder.Entity<CandidateCertificate>(entity =>
            {
                entity.HasKey(e => new { e.CandidateId, e.CertificateId });

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.CandidateCertificate)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateCertificate_CandidateBackground");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.CandidateCertificate)
                    .HasForeignKey(d => d.CertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CandidateCertificate_Certificate");
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.Property(e => e.CertificateName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CertificateValidFrom).HasColumnType("date");

                entity.Property(e => e.CertificateValidTo).HasColumnType("date");
            });

            modelBuilder.Entity<College>(entity =>
            {
                entity.Property(e => e.CollegeId).HasColumnName("CollegeID");

                entity.Property(e => e.CollegeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<InterviewResult>(entity =>
            {
                entity.HasKey(e => new { e.InterviewId, e.OperatorId });

                entity.HasIndex(e => e.RequiredAbilityPointId)
                    .HasName("UK_InterviewResult_RequiredAbilityPointID")
                    .IsUnique();

                entity.Property(e => e.InterviewId).HasColumnName("InterviewID");

                entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

                entity.Property(e => e.RequiredAbilityPointId).HasColumnName("RequiredAbilityPointID");

                entity.HasOne(d => d.Interview)
                    .WithMany(p => p.InterviewResult)
                    .HasForeignKey(d => d.InterviewId)
                    .HasConstraintName("FK_InterviewScheduleOperator_InterviewSchedule");

                entity.HasOne(d => d.Operator)
                    .WithMany(p => p.InterviewResult)
                    .HasForeignKey(d => d.OperatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewScheduleOperator_Operator");

                entity.HasOne(d => d.RequiredAbilityPoint)
                    .WithOne(p => p.InterviewResult)
                    .HasForeignKey<InterviewResult>(d => d.RequiredAbilityPointId)
                    .HasConstraintName("FK_InterviewResult_RequiredAbilityPoint");
            });

            modelBuilder.Entity<InterviewSchedule>(entity =>
            {
                entity.HasKey(e => e.InterviewId);

                entity.Property(e => e.InterviewId).HasColumnName("InterviewID");

                entity.Property(e => e.ApplyDetailId).HasColumnName("ApplyDetailID");

                entity.Property(e => e.Comment).HasMaxLength(200);

                entity.Property(e => e.InterviewDate).HasColumnType("date");

                entity.Property(e => e.InterviewTime).HasColumnType("time(4)");

                entity.HasOne(d => d.ApplyDetail)
                    .WithMany(p => p.InterviewSchedule)
                    .HasForeignKey(d => d.ApplyDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InterviewSchedule_ApplyDetail");
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("Unique_Operator_UserID")
                    .IsUnique();

                entity.Property(e => e.OperatorId).HasColumnName("OperatorID");

                entity.Property(e => e.OperatorName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<RequiredAbility>(entity =>
            {
                entity.Property(e => e.RequiredAbilityId).HasColumnName("RequiredAbilityID");

                entity.Property(e => e.AbilityId).HasColumnName("AbilityID");

                entity.Property(e => e.RequirementId).HasColumnName("RequirementID");

                entity.HasOne(d => d.Ability)
                    .WithMany(p => p.RequiredAbility)
                    .HasForeignKey(d => d.AbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequiredAbility_Ability");

                entity.HasOne(d => d.Requirement)
                    .WithMany(p => p.RequiredAbility)
                    .HasForeignKey(d => d.RequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequiredAbility_Requirement");
            });

            modelBuilder.Entity<RequiredAbilityPoint>(entity =>
            {
                entity.HasIndex(e => e.ApplyDetailAbilityId)
                    .HasName("UK_RequiredAbilityPoint_ApplyDetailAbilityID")
                    .IsUnique();

                entity.Property(e => e.RequiredAbilityPointId).HasColumnName("RequiredAbilityPointID");

                entity.Property(e => e.ApplyDetailAbilityId).HasColumnName("ApplyDetailAbilityID");

                entity.Property(e => e.RequiredAbilityId).HasColumnName("RequiredAbilityID");

                entity.HasOne(d => d.ApplyDetailAbility)
                    .WithOne(p => p.RequiredAbilityPoint)
                    .HasForeignKey<RequiredAbilityPoint>(d => d.ApplyDetailAbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequiredAbilityPoint_ApplyDetailAbility");

                entity.HasOne(d => d.RequiredAbility)
                    .WithMany(p => p.RequiredAbilityPoint)
                    .HasForeignKey(d => d.RequiredAbilityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequiredAbilityPoint_RequiredAbility");
            });

            modelBuilder.Entity<Requirement>(entity =>
            {
                entity.Property(e => e.RequirementId).HasColumnName("RequirementID");

                entity.Property(e => e.ApplyPositionId).HasColumnName("ApplyPositionID");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Language)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.RecruitFrom).HasColumnType("date");

                entity.Property(e => e.RecruitTo).HasColumnType("date");

                entity.Property(e => e.RequireDescription).HasMaxLength(2000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.WorkPlace)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.ApplyPosition)
                    .WithMany(p => p.Requirement)
                    .HasForeignKey(d => d.ApplyPositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requirement_ApplyPosition");
            });

            modelBuilder.Entity<RequirementCertificate>(entity =>
            {
                entity.HasKey(e => new { e.RequirementId, e.CertificateId });

                entity.Property(e => e.RequirementId).HasColumnName("RequirementID");

                entity.Property(e => e.CertificateId).HasColumnName("CertificateID");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.RequirementCertificate)
                    .HasForeignKey(d => d.CertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequirementCertificate_Certificate");

                entity.HasOne(d => d.Requirement)
                    .WithMany(p => p.RequirementCertificate)
                    .HasForeignKey(d => d.RequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RequirementCertificate_Requirement");
            });

            modelBuilder.Entity<SavedRequirement>(entity =>
            {
                entity.HasKey(e => new { e.CandidateId, e.RequirementId });

                entity.Property(e => e.CandidateId).HasColumnName("CandidateID");

                entity.Property(e => e.RequirementId).HasColumnName("RequirementID");

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.SavedRequirement)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedRequirement_Candidate");

                entity.HasOne(d => d.Requirement)
                    .WithMany(p => p.SavedRequirement)
                    .HasForeignKey(d => d.RequirementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedRequirement_Requirement");
            });

            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }
    }
}
