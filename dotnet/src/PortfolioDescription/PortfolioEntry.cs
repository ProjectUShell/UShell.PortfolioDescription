using System.Collections.Generic;

namespace UShell {

  public class PortfolioEntry {
    public string Label { get; set; } = "";
    public string PortfolioUrl { get; set; } = "";
    public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();
  }

}
