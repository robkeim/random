all: functions.o
	g++ p3.cpp functions.o -o p3exe

functions: functions.cpp
	g++ -c functions.cpp
	
submit:
	rm -rf *.o p3exe submission.tar.gz
	tar -zcf submission.tar.gz *

clean:
	rm -rf *.o p3exe submission.tar.gz
