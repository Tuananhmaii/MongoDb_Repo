﻿using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Domain.Interface.Service
{
    public interface IExcelService
    {
        public (IEnumerable<UserSkill> skillList, IEnumerable<SkillProperty> propertiesList,int filesExtracted) ExtractEvaluationData(IEnumerable<KeyValuePair<string, Stream>> files,string authorId);
    }
}
