func MyFunc(myParam) {
	myParam = myParam * 2
	MyInt = myParam
}
event OnFlagClicked {
	MyInt = 5
	if MyInt > 2 or 3 < MyInt + 4 {
		MyInt = 10
	}
}