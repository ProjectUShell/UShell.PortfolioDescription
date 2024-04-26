
namespace UShell {

  public class IDynamicParamMappingEntry {

   /**
   * based on this [EPIC](https://github.com/ProjectUShell/UShell.Docs/blob/master/epics/mapDynamic-Approach.md)
   * there can be the following:
   *  factory://UUID
   *  factory://UID64
   *  factory://now
   *  setting://GROUP.FIELD
   *   commandArgs://FIELD[.PROP[...]]
   *   unitOfWork://FIELD[.PROP[...]]
   *   applicationScope://FIELD[.PROP[...]]
   *   userInput://FIELD[.PROP[...]]
   */
    public string Use { get; set; }

    /**
   * the name of the target property on the paremter object,
   * that should be overwritten
   */
    public string For { get; set; }

  }

}
