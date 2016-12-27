	lw	0	1	n		$1 n input to function
	lw	0	2	r		$2 r input to function
	lw	0	6	fAdr		$6 temp variable
	jalr	6	7			$7 return address 
	halt
f	beq	2	0	end		base case (r == 0)
	beq	1	2	end		base case (n == r)
	sw	5	7	stack		store return address onto stack
	lw	0	6	one		$6 temp = 1
	add	6	5	5		increment stack pointer
	sw	5	1	stack		store n onto stack
	add	6	5	5		increment stack pointer
	sw	5	2	stack		store r onto stack
	add	6	5	5		increment the stack
	sw	5	4	stack		store local var onto stack
	add	6	5	5		increment stack pointer
	lw	0	6	nOne		$6 = -1
	add	1	6	1		n = n -1
	lw	0	6	fAdr		$6 = function address
	jalr	6	7			comb(n - 1, r)
	add	0	3	4		$4 local var = return value from call
	lw	0	6	nOne		$6 = -1
	add	2	6	2		r = r -1
	lw	0	6	fAdr		$6 = function address
	jalr	6	7			comb(n - 1, r - 1)
	add	3	4	3		comb (n, r) = comb(n - 1, r) + comb(n - 1, r - 1)
	lw	0	6	nOne		$6 = -1
	add	6	5	5		decrement stack pointer	
	lw	5	4	stack		load local var from stack			
	add	6	5	5		decrement stack pointer	
	lw	5	2	stack		load r from stack
	add	6	5	5		decrement stack pointer	
	lw	5	1	stack		load n from stack				
	add	6	5	5		decrement stack pointer
	lw	5	7	stack		load return address from stack
	jalr	7	6	
end	lw	0	3	one		$3 return address
	jalr	7	6				
one	.fill	1
nOne	.fill	-1
fAdr	.fill	f	
n	.fill	7
r	.fill 3
stack	.fill	0				value of stack is arbitrary
