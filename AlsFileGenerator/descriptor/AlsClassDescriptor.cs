using System;
using System.Collections.Generic;

namespace AlsFileGenerator.descriptor
{
  [Serializable()]
  public class AlsClassDescriptor
  {
    public String ClassName { get; set; }

    public String TagName { get; set; }

    public String InheritFrom { get; set; }

    public bool IsInterface { get; set; }

    public List<AlsSimpleAttributeDescriptor> SimpleAttributes { get; set; }

    public List<AlsComplexAttributeDescriptor> ComplexAttributes { get; set; }

    public List<AlsListDescriptor> ListAttributes { get; set; }


  }
}
