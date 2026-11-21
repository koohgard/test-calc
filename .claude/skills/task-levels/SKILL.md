# Task levels

Role key: `task_extractor` reads this file to choose the level of each task. Shared rules: read `rules/general.md`
and `rules/writing-ste100.md` of the Kook tool first. A project version in `kook-state/projects/<project>/skills/`
replaces this file at run time. The owner edits the levels in Settings > Roles > Task levels; the panel then keeps
one `## <id> · <title>` section for each level here, easiest first.

Use only the level ids that your prompt lists, in the order it gives (easiest first). When a task fits two levels,
choose the harder one. The level picks the agent of the coder (and of the reviewer and the fixer when the owner set
them per level), so a level that is too easy gives a hard task to a weak model. If a level of your prompt has no
section here, use its title and its place in the order.

## L1 · Trivial

A change in one file that copies a pattern that is already in the repo: a rename, a new field, a text or a config value, a small test. No design decision.

## L2 · Simple

A small change in one area, one to three files, that follows the existing design: a new method or endpoint like the ones next to it, with its tests.

## L3 · Complex

A change in several files or layers, or new logic with branches and edge cases. A small local design decision is necessary.

## L4 · Hard

A risky or unclear change: many files, a new design, concurrency, security, data migrations or a change to a shared interface. Mistakes are expensive.

## UIL1 · UI

Use this level for a task whose main work is in the user interface, and that is easy: a change that follows the existing components and styles. For example: a new field, label, column or button on an existing page, a small form, a text, color or spacing fix on one screen. One to three files. No new component, no new shared state and no new data loading.

## UIL2 · Hard UI

Use this level for a task whose main work is in the user interface, and that is hard: new UI design or new UI behavior. For example: a new page or a new reusable component, a form with validation rules, state that is shared between components, data loading with loading, error and empty states, a layout for many screen sizes, accessibility, or animation. Many files, or a change to a shared component that other pages use. When a UI task fits UIL1 and UIL2, choose UIL2.

# Projects with the four older levels

A kook.yaml without `levels:` has four levels: L1 · Trivial (one file, copy a pattern, no design), L2 · Simple (one
area, one to three files, existing design), L3 · Complex (several files or layers, a small local design decision)
and L4 · Hard (risky or unclear, many files, new design, shared interfaces).
