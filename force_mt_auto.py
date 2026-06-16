import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # Find the pagination container block that starts with <div class="px-6 py-4 border-t
    # or something similar, and make sure it has ' mt-auto '
    
    # We can match:
    # <div class="px-6 py-4 border-t border-[var(--border-secondary)] flex items-center justify-between bg-[var(--bg-primary)]">
    # and add shrink-0 mt-auto
    
    content = re.sub(
        r'(<div class="[^"]*border-t[^"]*flex items-center justify-between[^"]*bg-\[var\(--bg-primary\)\])(">)',
        r'\1 shrink-0 mt-auto\2',
        content
    )
    
    # Clean up multiple mt-auto or shrink-0 just in case
    content = content.replace(' shrink-0 mt-auto shrink-0 mt-auto', ' shrink-0 mt-auto')
    content = content.replace(' shrink-0 mt-auto shrink-0', ' shrink-0 mt-auto')
    content = content.replace(' mt-auto mt-auto', ' mt-auto')

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Added mt-auto: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
