﻿using System.Text.Json.Serialization;

namespace WheelWizard.Models.Github;

public class GithubRelease
{
    [JsonPropertyName("tag_name")]
    public string TagName { get; set; }
    [JsonPropertyName("assets")]
    public GithubAsset[] Assets { get; set; }
    
    [JsonPropertyName("prerelease")]
    public bool Prerelease{ get; set; }
}

public class GithubAsset
{
    [JsonPropertyName("browser_download_url")]
    public string BrowserDownloadUrl { get; set; }
}
