﻿namespace AssetManager.Shared.Parameters
{
    public class QueryParameter
    {
        public int? Id { get; set; } 
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
    }
}
