import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content
    
    # Only transform pages that have tables
    if '<table' not in content:
        return

    # 1. Page padding: reduce to maximize density
    content = re.sub(r'<div class="p-6 md:p-8">', r'<div class="p-2 md:p-4">', content)
    content = re.sub(r'<div class="mb-6 flex', r'<div class="mb-2 flex', content)
    content = re.sub(r'<div class="mb-6">', r'<div class="mb-2">', content)

    # 2. Card styling: remove rounded corners and shadow, make it sharp
    content = re.sub(r'rounded-xl shadow-sm', r'rounded-sm shadow-none', content)

    # 3. Filter/Search/Pagination row padding
    content = re.sub(
        r'px-6 py-4 border-([bt]) border-\[var\(--border-secondary\)\]',
        r'px-3 py-2 border-\1 border-[var(--border-secondary)]',
        content
    )

    # 4. Table cell headers
    content = re.sub(
        r'<th class="py-3 px-4(.*?)"',
        r'<th class="py-1.5 px-2 text-[11px] border-r border-[var(--border-secondary)] whitespace-nowrap\1"',
        content
    )

    # 5. Table data cells
    content = re.sub(
        r'<td class="py-3 px-4(.*?)"',
        r'<td class="py-1 px-2 text-[12px] border-r border-[var(--border-secondary)] whitespace-nowrap\1"',
        content
    )
    
    # 6. Table row striping
    content = re.sub(
        r'<tr class="border-b border-\[var\(--border-secondary\)\] hover:bg-\[var\(--bg-tertiary\)\]/50">',
        r'<tr class="border-b border-[var(--border-secondary)] hover:bg-[var(--bg-tertiary)] even:bg-[var(--bg-secondary)]/40 transition-none">',
        content
    )
    content = re.sub(
        r'<tr class="border-b border-\[var\(--border-secondary\)\]">',
        r'<tr class="border-b border-[var(--border-secondary)] hover:bg-[var(--bg-tertiary)] even:bg-[var(--bg-secondary)]/40 transition-none">',
        content
    )

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Transformed to dense grid: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
