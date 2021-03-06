﻿namespace Don2018.EventCloud.EntityFrameworkCore.Seed.Host
{
    public class InitialHostDbBuilder
    {
        private readonly EventCloudDbContext _context;

        public InitialHostDbBuilder(EventCloudDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            new DefaultEditionCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
            new InitialEventsCreator(_context).Create();

            _context.SaveChanges();
        }
    }
}
