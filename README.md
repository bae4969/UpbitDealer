
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

						Upbit Dealer ver.1.2.5

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

		IDE        :	Visual Studio 2019
		Language   :	c# Winform (.NET FrameWork)
		Include    :	upbit_API
		Require    :	Jwt package, json package

//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	Notice

		This project contain the latest version of upbit dealer. Currently, it is mainly undergoing
		long-term testing. And using test results, I change the macro algorithm.


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	Precautions
	
		1. This DOES NOT solve everything. Eventually, the price goes up and then gain profit.

		2. Above all, DO NOT trust this program. IF YOU LOSE MONEY, IT'S 'YOUR FAULT' TO USE IT.

		3. I tring to do not make erros and test several times, but don't trust it entirely as
		there may be errors. So, DO NOT perform strange manipulations, which increases the
		possibility of errors. That is a useless act and only your damage will occur.
	
		4. Characteristic of upbit api is it returns accurate results, but the number of requests
		per second is very low. Api limit are based on ip address and account. So DO NOT open
		'trader' and many 'chart' at the same time.

		5. Upbit api always need access ip address and your ip address sometimes change. That
		means, if you can not login one day, check your current ip address and change the access
		ip address in upbit homepage. Also, the key has an end period and you should be checked


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


	Can List

		1. Can show graph using API candle data

		2. Can trade (order or cancel)Above all, don't trust this program. If you lose it is your responsibility to use it.

		3. can lockup trade history

		4. Can use macro with bollinger value for top 70 coin

		5. Can show average weighted and average bollinger value


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	Update

		1. change the algorithm of api, added lost cut and auto bollinger value option

		2. add list of hot coin and danger coin

		3. Error#1 maybe fix

		
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	Detected Errors

		1. When it runs for a long period of time, memory usage keeps increasing.

		
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	How to build

		1. before build with visual studio, install package

		2. and build

		2. or install with 'UpbitDealer_setup.msi' file 


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	How to Use


		1. 


//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
