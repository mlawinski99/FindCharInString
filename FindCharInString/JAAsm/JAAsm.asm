.code
FindCharAsm proc
    mov rbx, rcx ; move string to rbx
    mov rax, r8 ; length of string
    sub rbx, 16 ; ; decrementing rbx, because we increment it in loop
    movq xmm1, rdx; 
    mov rdx, 1 ; length of char
L1:
    add rbx, 16 ; moving forward, 128 bit register = 16 bytes
    movdqu xmm0, xmmword ptr[rbx] ; moving string to xmm0
    pcmpestri  xmm0,xmm1, 0000b ; looking for any equal byte constant : 0000b = equal any
    jc Found ; if char is found
    sub rax, 16 ; decrementing counter
    jg L1 ;if string counter is equal or greater than 0
    ;if char is not found we return 0
    mov rax, 0
    ret
Found: ;if char is found we return 1
    mov rax, 1
    ret
FindCharAsm endp
end

