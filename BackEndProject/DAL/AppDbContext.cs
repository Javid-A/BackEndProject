using BackEndProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.DAL
{
	public class AppDbContext:IdentityDbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
		{
		}
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Course> Courses { get; set; }
		public DbSet<CourseContent> CourseContents { get; set; }
		public DbSet<CourseFeature> CourseFeatures { get; set; }
		public DbSet<Teacher> Teachers { get; set; }
		public DbSet<TeacherSkill> TeacherSkills { get; set; }
		public DbSet<TeacherContact> TeacherContacts { get; set; }
		public DbSet<Blog> Blogs { get; set; }
		public DbSet<Event> Events { get; set; }
		public DbSet<Speaker> Speakers { get; set; }
		public DbSet<EventSpeaker> EventSpeakers { get; set; }
		public DbSet<Bio> Bios { get; set; }
		public DbSet<Notice> Notices { get; set; }
		public DbSet<Testimonial> Testimoinals { get; set; }
		public DbSet<Reply> Replies { get; set; }
	}
}
