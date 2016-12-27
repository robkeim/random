all: functions.o
	g++ p5.cpp functions.o -o p5exe

functions: functions.cpp
	g++ -c functions.cpp
	
submit:
	rm -rf *.o p4exe submission.tar.gz
	tar -zcf submission.tar.gz *

clean:
	rm -rf *.o p4exe submission.tar.gz
