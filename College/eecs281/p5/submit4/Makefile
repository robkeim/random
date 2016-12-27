all: functions.o
	g++ p5.cpp functions.o -o p5exe -O3

functions: functions.cpp
	g++ -c functions.cpp
	
submit:
	rm -rf *.o p5exe submission.tar.gz
	tar -zcf submission.tar.gz *

clean:
	rm -rf *.o p5exe submission.tar.gz
