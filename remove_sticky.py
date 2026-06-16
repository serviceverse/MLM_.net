import os
import re

def update_file(filepath):
    with open(filepath, 'r', encoding='utf-8') as f:
        content = f.read()

    original_content = content

    # 1. Strip min-h-screen, flex flex-col from outer wrapper
    content = re.sub(
        r'<div class="min-h-screen bg-\[var\(--bg-secondary\)\] text-\[var\(--text-primary\)\] p-6 md:p-8 flex flex-col">',
        r'<div class="p-6 md:p-8">',
        content
    )
    
    # 2. Strip flex, flex-col, flex-1, overflow-hidden from Main Card
    content = re.sub(
        r'<div class="bg-\[var\(--bg-primary\)\] border border-\[var\(--border-secondary\)\] rounded-xl shadow-sm flex flex-col flex-1 overflow-hidden">',
        r'<div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm">',
        content
    )
    
    # Also catch any Main Cards that didn't have the exact flex classes
    content = re.sub(
        r'<div class="bg-\[var\(--bg-primary\)\] border border-\[var\(--border-secondary\)\] rounded-xl shadow-sm">',
        r'<div class="bg-[var(--bg-primary)] border border-[var(--border-secondary)] rounded-xl shadow-sm">',
        content
    )

    # 3. Strip flex-1 and bg from Table Content container
    content = re.sub(
        r'<div class="flex-1 overflow-auto bg-\[var\(--bg-primary\)\]">',
        r'<div class="overflow-x-auto">',
        content
    )
    
    # 4. Strip sticky from table header
    content = re.sub(
        r'<thead class="bg-\[var\(--bg-tertiary\)\] sticky top-0 z-10 shadow-sm">',
        r'<thead class="bg-[var(--bg-tertiary)]">',
        content
    )
    
    # 5. Fix Search/Filters row padding (strip flex-1 or weird classes if any)
    content = re.sub(
        r'<div class="px-6 py-4 border-b border-\[var\(--border-secondary\)\] flex items-center justify-between bg-\[var\(--bg-primary\)\] shrink-0">',
        r'<div class="px-6 py-4 border-b border-[var(--border-secondary)] flex flex-wrap items-center justify-between gap-4">',
        content
    )
    content = re.sub(
        r'<div class="px-6 py-4 border-b border-\[var\(--border-secondary\)\] flex items-center justify-between bg-\[var\(--bg-primary\)\]">',
        r'<div class="px-6 py-4 border-b border-[var(--border-secondary)] flex flex-wrap items-center justify-between gap-4">',
        content
    )
    content = re.sub(
        r'<div class="px-6 py-4 border-b border-\[var\(--border-secondary\)\] flex justify-between items-center bg-\[var\(--bg-primary\)\]">',
        r'<div class="px-6 py-4 border-b border-[var(--border-secondary)] flex flex-wrap justify-between items-center gap-4">',
        content
    )

    # 6. Fix Pagination row (strip mt-auto, bg-primary, shrink-0)
    # The previous regex added shrink-0 mt-auto. We want to remove it.
    content = re.sub(
        r'<div class="px-6 py-4 border-t border-\[var\(--border-secondary\)\] flex items-center justify-between bg-\[var\(--bg-primary\)\] shrink-0 mt-auto">',
        r'<div class="px-6 py-4 border-t border-[var(--border-secondary)] flex flex-wrap items-center justify-between gap-4">',
        content
    )
    content = re.sub(
        r'<div class="px-6 py-4 border-t border-\[var\(--border-secondary\)\] flex items-center justify-between bg-\[var\(--bg-primary\)\]">',
        r'<div class="px-6 py-4 border-t border-[var(--border-secondary)] flex flex-wrap items-center justify-between gap-4">',
        content
    )

    # Fix poorly formatted pages like Mt5List that didn't get standard pagination/search formatting
    content = re.sub(
        r'<div class="flex items-center justify-between mb-6">\s*<div class="flex items-center space-x-2">\s*<span class="text-\[var\(--text-secondary\)\]">Show</span>',
        r'<div class="px-6 py-4 border-b border-[var(--border-secondary)] flex flex-wrap items-center justify-between gap-4">\n                <div class="flex items-center space-x-2">\n                    <span class="text-[var(--text-secondary)]">Show</span>',
        content
    )
    
    content = re.sub(
        r'<div class="flex items-center justify-between mt-6">\s*<div class="text-gray-300">Page 1 of 1</div>',
        r'<div class="px-6 py-4 border-t border-[var(--border-secondary)] flex flex-wrap items-center justify-between gap-4">\n                <div class="text-[var(--text-secondary)]">Page 1 of 1</div>',
        content
    )

    if content != original_content:
        with open(filepath, 'w', encoding='utf-8') as f:
            f.write(content)
        print(f"Standardized layout: {filepath}")

def main():
    views_dir = r"C:\Users\Kuter\Documents\.net\MLM_.net\Views"
    for root, dirs, files in os.walk(views_dir):
        for file in files:
            if file.endswith('.cshtml'):
                update_file(os.path.join(root, file))

if __name__ == "__main__":
    main()
