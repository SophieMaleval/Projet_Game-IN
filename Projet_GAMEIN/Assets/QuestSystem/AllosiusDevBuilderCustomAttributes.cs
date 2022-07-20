using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]

public class BaseCustomAttribute : Attribute { }

public class IDAttribute : BaseCustomAttribute { }

public class TaskIDAttribute : IDAttribute { }

public class AllosiusDevDataListAttribute : BaseCustomAttribute { }
