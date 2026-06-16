import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # 1. Wrap form in a standard card
    content = re.sub(
        r'<div class="flex-1 overflow-hidden flex flex-col">\s*(<form[^>]*>)',
        r'<div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm p-6 md:p-8">\n            \1',
        content
    )

    # 2. Fix the header if it was a weird banner
    # <div class="px-6 py-4 bg-[var(--bg-secondary)] border-b border-[var(--border-primary)] flex justify-between items-center shrink-0">
    content = re.sub(
        r'<div class="px-6 py-4 bg-\[var\(--bg-secondary\)\] border-b border-\[var\(--border-primary\)\] flex justify-between items-center shrink-0">',
        r'<div class="mb-6">',
        content
    )
    
    # 3. Fix the h1 in the headers so it looks like the standard
    # e.g. <h1 class="text-4xl font-bold mb-2 text-[var(--text-primary)]"> or <h1 class="text-2xl font-bold">
    content = re.sub(
        r'<h1 class="text-4xl font-bold text-\[var\(--text-primary\)\] mb-2">',
        r'<h1 class="text-2xl font-bold text-[var(--text-primary)]">',
        content
    )
    content = re.sub(
        r'<h1 class="text-4xl font-bold mb-2 text-\[var\(--text-primary\)\]">',
        r'<h1 class="text-2xl font-bold text-[var(--text-primary)]">',
        content
    )

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Fixed: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
