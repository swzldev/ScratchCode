event OnFlagClicked {
	MyInt = 0
	repeatuntil MyInt == 10 and 50 > MyInt {
		MyInt = MyInt + 1
		Wait(0.5)
	}
}