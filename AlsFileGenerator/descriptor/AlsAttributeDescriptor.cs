using System;

namespace AlsFileGenerator.descriptor
{
  [Serializable()]
  public class AlsAttributeDescriptor
  {
    public String Type { get; set; }

    public String TagName { get; set; }

    public String AttributeName { get; set; }
  }
}
