using System;
using System.Collections.Generic;

namespace AlsFileGenerator.descriptor
{
  [Serializable()]
  public class AlsListDescriptor : AlsAttributeDescriptor
  {
    public List<ListPossibleValue> PossibleContent { get; set; }
  }
}
