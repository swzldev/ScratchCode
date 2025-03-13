func MyFunc(myParam) {
	MoveSteps(myParam)
}
event OnFlagClicked {
	MyBool = false
	MyBool = true
	MyFunc(MyBool)
	myParam2 = 10
}
event OnFlagClicked {
	
}