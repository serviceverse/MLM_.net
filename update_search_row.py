import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content
    
    # 1. Add padding to the Search Row so it doesn't touch the screen edge
    content = re.sub(
        r'<!-- Search Row -->\s*<div class="flex justify-between items-center mb-6">',
        r'<!-- Search Row -->\n            <div class="flex justify-between items-center mb-4 px-6 py-4 bg-[var(--bg-primary)] border-b border-[var(--border-secondary)]">',
        content
    )

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Updated Search Row: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
