func MyFunc(myParam) {
	MoveSteps(myParam)
}
event OnFlagClicked {
	MyBool = false
	MyBool = true
	MyFunc(MyBool)
	myParam = 10
}
event OnFlagClicked {
	
}