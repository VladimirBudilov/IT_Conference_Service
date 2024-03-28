﻿using IT_Conference_Service.Data.Entitiess;
using IT_Conference_Service.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IT_Conference_Service.Data.Repositories
{
    public class SpeackerInfoRepository : BaseRepository<AuthorInfo>, ISpeackerInfoRepository
    {
        public SpeackerInfoRepository(ConferenceDbContext context) : base(context)
        {
        }
    }
}
