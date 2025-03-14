# Functions
Functions are blocks of code that can be reused throughout your program. They can take an optional number of parameters and return a value to the caller. In Scratch, they are equivelent to "My Blocks", the only difference being an optional return value passed through the internal @vfuncreturn variable.

## Function Declerations

### Syntax
```
func MyFunc(param1, param2) {
    ...
}
```

### Errors
- **CS8**: The function already has a decleration. This may be because you are defining multiple functions with the same name, or you are defining a function that has the same name as a built-in function.
- **CS15**: Functions cannot be declared inside other functions.


## Function calls

### Syntax
```
MyFunc(1, 2)
```

### Errors


# Built-in functions
There are a number of built-in functions that equate to the various blocks available in Scratch. A comprehensive list can be found below.

## MoveSteps
Moves a sprite by a number of steps

### Syntax
```
MoveSteps(steps)
```

### Parameters
- **steps**: The number of steps to move.

### Notes
Only available on non-scene sprites


## TurnRight
Moves a sprite by a number of steps

### Syntax
```
TurnRight(degrees)
```

### Parameters
- **degrees**: The number of degrees to turn.

### Notes
Only available on non-scene sprites


## TurnLeft
Moves a sprite by a number of steps

### Syntax
```
TurnLeft(degrees)
```

### Parameters
- **degrees**: The number of degrees to turn.

### Notes
Only available on non-scene sprites


## GoTo
Moves a sprite by a number of steps

### Syntax
```
GoTo(to)
```

### Parameters
- **to**: The place to go to.

### Notes
Only available on non-scene sprites