import os

def clear_screen():
    os.system('cls' if os.name == 'nt' else 'clear')

def center_text(text, width, height):
    lines = text.split('\n')
    centered_lines = [line.center(width) for line in lines]
    vertical_padding = (height - len(lines)) // 2
    return '\n' * vertical_padding + '\n'.join(centered_lines)

def main():
    width, height = 80, 25
    text = "Console Incremental"
    
    clear_screen()
    centered_text = center_text(text, width, height)
    print(centered_text)

if __name__ == "__main__":
    main()
