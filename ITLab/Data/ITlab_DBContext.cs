using System;
using ITLab.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static ITLab.Models.ItlabUser;

namespace ITLab.Models
{
    public partial class ITLab_DBContext : DbContext
    {
        public ITLab_DBContext()
        {
        }

        public ITLab_DBContext(DbContextOptions<ITLab_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AttendeeUser> AttendeeUser { get; set; }
        public virtual DbSet<Classroom> Classroom { get; set; }
        public virtual DbSet<Feedback> Feedback { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<ItlabUser> ItlabUser { get; set; }
        public virtual DbSet<RegisterdUser> RegisterdUser { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<SessionCalendar> SessionCalendar { get; set; }
        public virtual DbSet<SessionMedia> SessionMedia { get; set; }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<AttendeeUser>(entity =>
            {
                entity.HasKey(e => new { e.SessionId, e.UserUsername })
                    .HasName("PK__Attendee__30F84A71354F7242");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.UserUsername)
                    .HasColumnName("user_username")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.AttendeeUser)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("AttendeeUsersession_id");

                entity.HasOne(d => d.UserUsernameNavigation)
                    .WithMany(p => p.AttendeeUser)
                    .HasForeignKey(d => d.UserUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ttendeeUserserusername");
            });

            modelBuilder.Entity<Classroom>(entity =>
            {
                EnumToStringConverter<Campus> converterCampus = new EnumToStringConverter<Campus>();
                EnumToStringConverter<RoomCategory> converterRoomcategory = new EnumToStringConverter<RoomCategory>();

                entity.HasKey(e => e.Classid)
                    .HasName("PK__Classroo__96D40B6C3B806A01");

                entity.Property(e => e.Classid)
                    .HasColumnName("CLASSID")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Campus).HasConversion(converterCampus)
                    .IsRequired()
                    .HasColumnName("CAMPUS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Maxseats)
                    .IsRequired()
                    .HasColumnName("MAXSEATS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Roomcategory).HasConversion(converterRoomcategory)
                    .IsRequired()
                    .HasColumnName("ROOMCATEGORY")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AuthorUsername)
                    .IsRequired()
                    .HasColumnName("AUTHOR_USERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Contenttext)
                    .IsRequired()
                    .HasColumnName("CONTENTTEXT")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.HasOne(d => d.AuthorUsernameNavigation)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.AuthorUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FeedbackAUTHORUSERNAME");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.Feedback)
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("FK_Feedback_session_id");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.Imagekey)
                    .HasName("PK__Image__ED2B567C00A2FF7D");

                entity.Property(e => e.Imagekey).HasColumnName("IMAGEKEY");

                entity.Property(e => e.Image1)
                    .IsRequired()
                    .HasColumnName("Image")
                    .HasColumnType("image");
            });

            modelBuilder.Entity<ItlabUser>(entity =>
            {
                EnumToStringConverter<UserStatus> converterStatus = new EnumToStringConverter<UserStatus>();
                EnumToStringConverter<UserType> converterType = new EnumToStringConverter<UserType>();

                entity.HasKey(e => e.Username)
                    .HasName("PK__ItlabUser__B15BE12FB744CF0C");

                entity.Property(e => e.Username)
                    .HasColumnName("USERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("FIRSTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasColumnName("LASTNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("PASSWORD")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserStatus).HasConversion(converterStatus)
                    .IsRequired()
                    .HasColumnName("USERSTATUS")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserType).HasConversion(converterType)
                    .IsRequired()
                    .HasColumnName("USERTYPE")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RegisterdUser>(entity =>
            {
                entity.HasKey(e => new { e.SessionId, e.UserUsername })
                    .HasName("PK__Register__30F84A71C1E37909");

                entity.Property(e => e.SessionId).HasColumnName("session_id");

                entity.Property(e => e.UserUsername)
                    .HasColumnName("user_username")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.RegisterdUser)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RegisterdUsersessionid");

                entity.HasOne(d => d.UserUsernameNavigation)
                    .WithMany(p => p.RegisterdUser)
                    .HasForeignKey(d => d.UserUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("RgisterdUsersrusername");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                EnumToStringConverter<State> converterState = new EnumToStringConverter<State>();


                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClassroomClassid)
                    .IsRequired()
                    .HasColumnName("CLASSROOM_CLASSID")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .HasColumnName("DESCRIPTION")
                    .IsUnicode(false);

                entity.Property(e => e.Endhour).HasColumnName("ENDHOUR");

                entity.Property(e => e.Eventdate)
                    .HasColumnName("EVENTDATE")
                    .HasColumnType("date");

                entity.Property(e => e.HostUsername)
                    .IsRequired()
                    .HasColumnName("HOST_USERNAME")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Maxattendee).HasColumnName("MAXATTENDEE");

                entity.Property(e => e.Nameguest)
                    .HasColumnName("NAMEGUEST")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Starthour).HasColumnName("STARTHOUR");

                entity.Property(e => e.Stateenum).HasConversion(converterState)
                    .HasColumnName("STATEENUM")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("TITLE")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Videourl)
                    .HasColumnName("VIDEOURL")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ClassroomClass)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.ClassroomClassid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SssionCLASSROOMCLASSID");

                entity.HasOne(d => d.HostUsernameNavigation)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.HostUsername)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Session_HOST_USERNAME");

                entity.HasOne(d => d.SessionCalendar)
                    .WithMany(p => p.Session)
                    .HasForeignKey(d => d.SessionCalendarId)
                    .HasConstraintName("SssionSssionCalendarId");
            });

            modelBuilder.Entity<SessionCalendar>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Enddate)
                    .HasColumnName("ENDDATE")
                    .HasColumnType("date");

                entity.Property(e => e.Startdate)
                    .HasColumnName("STARTDATE")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<SessionMedia>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Session_MEDIA");

                entity.Property(e => e.Media).HasColumnName("MEDIA");

                entity.Property(e => e.SessionId).HasColumnName("Session_ID");

                entity.HasOne(d => d.Session)
                    .WithMany()
                    .HasForeignKey(d => d.SessionId)
                    .HasConstraintName("SessionMEDIASession_ID");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
