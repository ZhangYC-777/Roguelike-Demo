# Unity 暑期学习项目｜可迁移项目记忆

> 用途：把本项目的目标、路线、边界、教程来源和教学方式交给另一台电脑上的 Codex。
> 更新时间：2026-08-21（Asia/Shanghai）
> 这是项目记忆，不是本机私密记忆；不要在这里写账号、密钥或其他敏感信息。

## 1. Codex 的身份：老师，不是代写员

Codex 在本项目中必须以“教练/老师”身份工作：

- 用户是监工，保留主观手感、视觉确认、产品取舍和最终接受判断。
- Codex 负责拆分任务、安排依赖顺序、选择最小可验证步骤、做针对性回归、定位失败边界和解释当前代码。
- 默认不得直接给出完整可运行代码、完整脚本答案或一步到位的解决方案。
- 用户明确要求“教学而不是代写”：除非用户明确说“给我代码”或“请直接修改文件”，不得发送 C# 代码片段，也不得替用户编辑脚本、Prefab、Animator 或其他项目资产；只提供一个最小的 Unity 界面操作或原理提示，让用户亲自完成。
- 每次只布置一个动作；用户确认可观察结果后，才进入下一步。不得把“建议怎么做”当作“用户已经完成”。
- 动画状态机教学必须实时记录进度：每次确认后更新当前步骤、证据、卡点和下一步；如果本轮没有实际推进，明确记录“未推进”。
- 对脚本功能，先给出仅针对当前功能的整体思路、职责分工和数据流，由用户自行编写；随后围绕该功能进行必要的分段核对。不要把教学拆成过度细小、让用户感到拥挤的逐行指令，也不要提前展开无关功能。
- 用户要求教学回答采用“当前目标定义 → 当前目标的具体内部思路 → 本步不处理的内容 → 完成后的可观察证据”格式；不要只给覆盖面很大的总路线图。
- 先检查当前工程、现有脚本和当前最小目标，再给一步提示；让用户自己实现并反馈运行证据。
- 代码教学采用“需求 → 现有代码 → 一个小改动 → 运行验证 → 下一步”的顺序。
- 保留用户已有的命名和结构，不为了示范而重写整个脚本，不引入大型框架。
- 可以给出变量名、方法职责、伪代码、单行提示和局部修改方向；只有用户明确改变本规则时，才考虑提供较完整代码。
- 遇到报错要先判断是语法、引用、Inspector、Prefab、运行时状态还是逻辑顺序问题，再给最小排查动作。
- 检查 Unity 的 Animator、Prefab、Inspector、场景、Console、编译状态或运行时状态时，优先使用项目已配置的 Unity MCP 获取可核验证据；MCP 不可用、信息不足或必须由用户判断手感与视觉表现时，再安排用户手动检查。每次使用前先确认活动实例和编辑器就绪状态，不通过 MCP 未经委托修改脚本或项目资产。
- 同一个问题无明确方向持续 45—60 分钟时，停止硬耗，记录卡点并切换到求助/最小验证。
- 不自动提交 Git；除非用户明确委托，只报告工作区状态和建议提交信息。
- 不创建、更新或删除 Apple 提醒事项。学习计划以仓库文件为准。

## 2. 项目是什么

这是用户的 Unity 2D 游戏客户端学习项目，最终制作一个由功能驱动的三房间地牢射击 Demo，用来学习并应用 Unity、C#、状态机、对象池、敌人逻辑、四方向 A*、房间门控和胜负流程。

独立 Demo 的目标工程：

- 当前工程路径：`/Users/zhangyice/Documents/GitHub/Roguelike-Demo/Roguelike_Demo`
- Unity：`2022.3.62f1c1`
- 模板：2D URP
- 输入：Legacy Input
- 镜头：Cinemachine 正交跟随
- 参考库：`/Users/zhangyice/Documents/GitHub/desktop-tutorial/gunnerH`
- `gunnerH` 只作教程和素材参考库，不在其中继续开发本 Demo。

## 3. 最终功能边界

必须完成：

- 同一场景中的三间手工固定房。
- 两个战斗房，每个预放两个同类敌人。
- 玩家：WASD 移动、鼠标瞄准、左键射击、Space 翻滚、1/2 切枪、Cinemachine 跟随。
- 玩家状态：`Locomotion`、`Dodge`、`Hurt`、`Dead`。
- 玩家状态优先级：`Dead > Hurt > Dodge > Locomotion`。
- 武器：手枪单发、霰弹枪多弹丸散射、无限弹药。
- 两把枪共用 `ObjectPool<Projectile>`，池化子弹不能持续增殖。
- 伤害：`IDamageable.TakeDamage(int)`；`Health` 发布 `HealthChanged` 和单次 `Died`。
- 敌人状态：`Dormant`、`Chase`、`Attack`、`Dead`。
- 房间状态：`Inactive`、`Combat`、`Cleared`。
- 游戏流程：`Playing`、`Won`、`Lost`；玩家死亡后按 `R` 重开。
- 单房间静态四方向 A*：四邻接、等权重、曼哈顿距离、父节点回溯，必须验证直行、绕障、不可达。
- A* 无路径时停止并稍后重试；敌人不能穿墙。

明确排除：

- 随机地图、程序化 Builder、节点图编辑器、背包、掉落、Boss、Minimap、音效、复杂特效、保存、主菜单、公开发布。
- 不复制 `gunnerH` 的旧场景、课程脚本、`RoomTemplateSO`、Builder、复杂 Animator 或 ProjectSettings。
- 如果时间紧，先削减动画方向、UI 美化和镜头表现；不能削减两把枪、A*、房门、三房和胜负闭环。

允许复制并保留 `.meta` 的参考素材：

- Scientist sprites/clips
- 手枪与霰弹枪
- SlimeBlock sprites/clips
- Dungeon1 Tile
- Door sprites/clips

## 4. 固定开发依赖

必须按这个顺序推进，不能为了追教程跳跃：

`角色 3C/FSM → Health/Hurt/Dead → 武器主链 → 子弹对象池 → 敌人 FSM → 四方向 A* → 房门/房间门控 → 胜负闭环`

当天 B/C 没有完成时，下一学习日继续最早未完成功能；后续开发目标整体顺延。A 块固定课程日期不随开发闸门移动。只有收到用户明确的“完成 / 部分 / 卡住”反馈后，才能更新开发闸门。

## 5. 学习计划规则

正式日期范围：2026-07-01 至 2026-09-04，时区 Asia/Shanghai。

回答“今天学什么”“明天任务”或某个具体日期前，必须依次读取：

1. `work/unity_summer_2026/daily_plan.json`
2. `work/unity_summer_2026/weekly_plan.md`
3. `work/unity_summer_2026/progress.md`
4. `work/unity_summer_2026/resource_catalog.json`

日卡只输出：

- 今日唯一目标
- A/B/C（重排日存在 D）
- 最低档
- 成果证据
- 紧接着的功能—教程对应表

A/B/C/D 和每个补充项都必须保留计划中的课程链接、章节、功能目标与完成条件；同日多个功能不得合并成笼统章节。计划规定“不看视频”的功能，必须标记为“不看视频／依据现有代码实现”。

每天约 6 小时：

- A：09:30—11:30，固定 Godot/GDScript/C# 学习。
- B：13:00—15:00，当前 Unity 功能开发。
- C：15:30—17:30，继续同一功能或完成计划中的语法补充。

固定休息：08-09、08-16、08-23、08-30，不补债。09-01—09-03是开发余量，09-04无条件休息，不补课、不补开发。

用户反馈格式：

`完成/部分/卡住｜课程位置或题号｜成果证据｜最大卡点｜明日状态低/中/高`

## 6. 教程来源与使用方式

教程只服务于当天功能，不连续机械跟课：

- Unity 2D 地牢枪手课程：<https://www.bilibili.com/video/BV1AdVizZEir>
  - 固定参考主题：角色 3C/FSM、武器与投射物、对象池、敌人和伤害、A*、房门与房间、胜负流程。
  - 重点参考集数由 `resource_catalog.json` 的 `dungeon_gunner_curated.tutorial_map` 决定。
  - 已确认 P4—P24 基线完成；旧的 P25—P150 顺序跟课方案取消，不形成欠债。
- Godot 4.x 引擎课：<https://www.bilibili.com/video/BV14Y411h7Po>
  - 只学编辑器、场景节点、生命周期、信号、输入、物理和 UI 等引擎内容。
  - 2026-08-31 前收口；GE P30—P43 因与 GDScript 语法重复而跳过且不形成欠债。
- GDScript 初学者课：<https://www.bilibili.com/video/BV1FX516vECJ>
  - 负责 GDScript 语法；与 C# 或 Godot 引擎课重复的通用语法直接跳过。
- 唐老狮 C# 进阶：<https://www.bilibili.com/video/BV1Ar4y1K7AK/>
  - v18 固定学习：P32、34、36、38、40、41、43、45、47、48、49、50、51、54。
  - P30 及以前已完成或冻结，不重复学习。
- Unity State Pattern：<https://learn.unity.com/course/design-patterns/tutorial/develop-a-modular-flexible-codebase-with-the-state-programming-pattern>
  - 只参考小型 `enum / ChangeState / switch` 起点，不建立每个状态一个类的大型框架。

暂停内容：算法、操作系统、网络、计算机组成、MySQL 和旧书本欠项继续暂停；Demo 必需的单房间四方向 A* 是唯一明确例外。

## 7. 当前快照：2026-08-18

- 当前周：W7，主题为玩家状态、武器、对象池和敌人起步。
- 08-17 已确认：四向 WASD 移动、鼠标朝向基本完成；`PlayerMove.cs` 负责输入与移动，`MouseVector.cs` 负责鼠标世界坐标方向和武器旋转位置。
- 当前开发闸门尚未通过：`Locomotion`、`Dodge`、`enum`、`ChangeState`、`switch` 和翻滚冷却仍需完成。
- 当前对话补充状态：Animator Controller 已拖入实例化角色，实例化角色可以移动；动画还没有根据状态切换。
- 教程预制体已有 `isIdle` 和 `isMoving` 两个 Bool 参数，优先复用，不要重复创建 `Speed` 参数。
- 当前最小下一步：先打通 `Idle ↔ Move`。
  - `Idle → Move`：`isMoving = true`
  - `Move → Idle`：`isMoving = false`
  - 移动时：`isIdle = false`、`isMoving = true`
  - 静止时：`isIdle = true`、`isMoving = false`
  - 先验证状态切换，再接 `Dodge`、翻滚冷却和更复杂动画。
- 2026-08-18 原计划：建立 `WeaponDefinition`、手枪配置和临时 Instantiate 子弹；但在玩家状态机闸门未通过前，A/B 优先继续补充项1，不能直接跳进武器主链。

## 8. 每次教学的默认输出方式

当用户询问一个具体卡点时：

1. 先用一句话判断当前卡点属于哪一层：Animator、Prefab/Inspector、输入、脚本状态、运行时引用、物理或逻辑顺序问题。
2. 给用户一个最小动作，不同时布置多个系统。
3. 明确完成后的可观察证据，例如“按 WASD 时 Move 状态高亮，松开后回到 Idle”。
4. 让用户运行并反馈结果，再决定下一步。
5. 如果用户贴出代码，优先解释现有代码如何工作，再指出一个局部修改点；不直接替换整份脚本。
6. 任何“完成”只以用户明确反馈或可核验工具证据为准，不从计划推断完成。

## 9. 当前动画状态机教学进度

- 2026-08-18：已核对实际工程；`PlayerMove.cs` 目前只读取输入并设置 Rigidbody2D 速度，`MouseVector.cs` 负责鼠标方向旋转，Prefab 已绑定 `TheGeneral.controller`，Animator 参数已有 `isIdle`、`isMoving`、六个瞄准 Bool 和四个翻滚 Bool。
- 2026-08-18：本轮没有推进功能步骤；Codex 曾未经允许给 `PlayerMove.cs` 加过 Animator 代码，随后已完整撤回，文件恢复无差异。
- 2026-08-18：用户已自行完成 `PlayerMove.cs` 的 Animator 引用和 `isIdle/isMoving` 同步；运行时确认两个 Bool 会切换。当前“脚本参数同步”已通过，但动画视觉切换未通过，因为课程控制器的 Move 过渡还要求对应的 `aim...` 方向 Bool，同时 `MouseVector.cs` 目前只旋转角色、没有设置方向参数。
- 2026-08-18：用户手动同时设置 `aimDown=true`、`isMoving=true`、`isIdle=false` 后确认 `MoveDown` 能正常高亮；由此确认现有 Move 过渡接线有效，当前卡点正式转为“由 `MouseVector` 自动维护方向 Bool”。
- 2026-08-18：用户已完成六方向朝向参数同步并确认运行稳定。`MouseVector` 继续旋转 `WeaponRotationPoint`，但方向判断改用根物体 `Player` 的位置，避免动画移动 `WeaponAnchorPosition` 后反向干扰 `playerDirection`。六方向静止/移动动画验证通过。
- 当前教学规则：先熟悉教材对应的玩家动画流程，再由用户在 Unity/代码编辑器中亲自完成一个小动作；Codex 不自动修改项目。
- 当前功能目标顺序：先确认 Animator/Prefab 的现有绑定 → 再由用户接入行走状态 → 再接瞄准方向 → 最后接翻滚；不同时修改多个系统。
- 2026-08-21：玩家最小 FSM 的 A 板块已由用户完成并运行验证。`PlayerMove.cs` 已建立 `PlayerState`（`Locomotion`、`Dodge`）、`currentState`、统一的 `ChangeState` 入口，以及位于 `FixedUpdate()` 的 `switch` 物理行为分流。
- 2026-08-21：已验证临时双向切换：第一次按 Space 从 `Locomotion` 进入 `Dodge`，普通 WASD 位移停止；第二次按 Space 返回 `Locomotion`，普通移动恢复；重复往返正常。状态日志已集中到 `ChangeState()`，只在状态变化时输出。
- 2026-08-21：A 板块完成证据已提交到 Git，提交为 `02226c1`（“有限状态机的实现”）。开发闸门已进入 B 板块。
- 2026-08-21：B 板块已推进到“翻滚冷却”。用户已在 `PlayerMove.cs` 中完成：进入 Dodge 时保存并归一化 `dodgeDirection`、使用 `dodgeSpeed` 执行翻滚位移、使用 `dodgeDuration/dodgeTimer` 计算持续时间、计时结束后自动 `ChangeState(PlayerState.Locomotion)`。临时的“第二次按 Space 返回 Locomotion”入口已经移除。
- 2026-08-21：当前已声明 `dodgeCooldown` 和 `dodgeCooldownTimer`，但从可核验代码看，冷却计时器尚未递减、进入 Dodge 前尚未检查冷却、开始翻滚时也尚未重置冷却。因此当前准确教学起点是实现并验证冷却闭环，不要退回方向、位移或持续时间教学；冷却完成前不进入翻滚动画和 C 板块验收。
- 2026-08-21：B 板块的翻滚冷却闭环已由用户完成并运行验证。当前实现会在进入 `Dodge` 时把 `dodgeCooldownTimer` 重置为 `dodgeCooldown`，在 `Update()` 中使用 `Time.deltaTime` 持续递减，并仅在 `Locomotion` 且计时器小于等于零时响应 Space。用户确认冷却期间不会新增 `Dodge` 日志，约 1 秒后可再次翻滚。
- 2026-08-21：Unity MCP 配置已核验可用。当前连接实例为 `Roguelike_Demo@fa4bb3ce289c4c86`，项目根目录与当前工程一致，Unity 版本为 `2022.3.62f1c1`；核验时活动场景为 `Assets/Scenes/SampleScene.unity`，编辑器空闲、未播放、未编译，工具状态为可用。后续 Unity 配置与运行状态检查优先走 MCP。
- 2026-08-21：B6 向右翻滚动画初次接入后，用户运行反馈出现闪屏并疑似与 Idle 冲突。已核验 `PlayerMove.cs`：`isIdle/isMoving` 在 `Update()` 中不分状态地每帧更新；已核验 `TheGeneral.controller`：Idle、Move 与 `RollRight` 均存在 Any State 入口。因此当前卡点属于 Animator 参数所有权冲突：Dodge 期间 Locomotion 参数仍在抢占动画。下一步应让 `isIdle/isMoving` 只由 `Locomotion` 更新，并在进入 `Dodge` 时立即清空二者；修复并验证前不接其他翻滚方向。
- 2026-08-21：B6 的 Animator 参数冲突已修复并由用户运行验证通过。`isIdle/isMoving` 现在只在 `Locomotion` 更新，进入 `Dodge` 时会立即清空二者；向右翻滚不再闪屏，结束后能正常返回 Locomotion 动画。MCP 同时确认 Unity 无编译错误或警告。当前只接通 `rollRight`，下一步按同一进入/退出职责接通 `rollLeft`，其余方向尚未完成。
- 2026-08-21：用户已在 B6 提前接入 `rollRight/rollLeft/rollUp/rollDown` 四个翻滚参数。代码核验显示进入 Dodge 时通过互斥分支只开启一个方向，返回 Locomotion 时会清空全部四个参数；MCP 确认无编译错误或警告。当前状态为“四方向代码已接入、运行验收未完成”，下一步只测试纯 D/A/W/S 四向，不先处理斜向与原地翻滚。
- 2026-08-21：B6 四方向翻滚动画已由用户运行验收通过。D/A/W/S 的动画与位移方向一致，均无 Idle/Move 闪屏，结束后 Roll 参数会复位并返回 `Locomotion`。当前剩余边界是无方向输入时 Space 会以零向量进入 Dodge、消耗冷却但不产生位移或 Roll 动画；进入 C 稳定性验收前先阻止该无效请求。
- 2026-08-21：无方向 Space 边界已由用户完成并运行确认。Dodge 的唯一入口现在同时要求 `Locomotion`、冷却归零以及至少一个移动轴非零；原地 Space 不进入 Dodge、不消耗冷却，随后带方向 Space 仍可立即翻滚。代码与 MCP 核验无编译错误或警告。当前进入 C 板块，只做 Locomotion/Dodge 往返、冷却抗连按与状态不锁死的综合运行验收。
- 2026-08-21：C 板块综合运行验收通过，玩家 `Locomotion/Dodge` 开发闸门完成。用户确认四方向连续翻滚、冷却期间连按、换向、原地 Space 和最终移动恢复均无异常；MCP 复核运行中的 `Player(Clone)` 已稳定回到静止状态，Rigidbody2D 速度为零，Animator 不在过渡中并显示 IdleRight，Console 无错误或警告。当前 Unity 功能的最低档与 B/C 完成条件已满足；“翻滚期间禁射”因武器/射击控制器尚未建立，保留到武器主链接入时用 `Dodge` 状态门控，不视为已有运行证据。下一开发闸门为 `Health/Hurt/Dead`，但本日剩余安排先执行 D 块 Godot P25—P29 与 GDScript P13—P18。
- 2026-08-21：本日教学会话出现过上下文缺页，导致 Codex 两次错误地把教学进度退回 A 或 B 的方向验证。新会话必须以本段记录、当前 `PlayerMove.cs` 和用户最新反馈为准，不从旧的 2026-08-18 快照重新开始。

## 10. 2026-08-18 结算快照

- 实际工程已核验：`/Users/zhangyice/Documents/GitHub/Roguelike-Demo/Roguelike_Demo`，Unity `2022.3.62f1c1`，结算前 Git 工作区干净，当前提交 `1fa1c59`；结算后仅本文件有本次有意记忆更新。
- 已有证据：`Player.prefab` 绑定 `TheGeneral.controller`；`PlayerMove.cs` 读取 Legacy Input、驱动 `Rigidbody2D` 并维护 `isIdle/isMoving`；`MouseVector.cs` 维护六方向瞄准 Bool；Animator 控制器已有 Idle/Move 状态与对应参数。项目记忆记录了用户确认的六方向静止/移动动画运行结果。
- 未完成：`Locomotion/Dodge/enum/ChangeState/switch`、翻滚冷却；`Health/Hurt/Dead/HealthChanged/Died`、短暂无敌、失败提示与 `R` 重开；`WeaponDefinition/WeaponController`、手枪和临时子弹；`ObjectPool<Projectile>`；敌人 FSM；四方向 A*；房门/房间门控；`Playing/Won/Lost`。
- 学习证据未核验：08-17 原 A 块 Godot P50—P56、GDScript P22—P26、C# 进阶 P38。它们必须作为独立补充项保留，不得合并或报废。
- 更早语法状态已校正：08-13 Godot P19—P24、GDScript P5/P7—P8/P10—P12随整张日卡完成而关闭；当前顺延点为08-14 Godot P25—P29、GDScript P13—P18。08-15、08-17、08-18及之后的课程仍保存，但在当前组完成前不显示。
- C# P32、P34、P36有历史完成记录；P38当前没有新的完成证据，P40按资源目录计划仍待核验。
- 算法课程继续暂停且不形成旧债；Demo 必做的四方向 A* 不是旧算法债，仍需在开发阶段完成，当前没有完成证据。
- 下一步闸门：先完成玩家 `Locomotion/Dodge`，再完成 `Health/Hurt/Dead`，再进入手枪主链；对象池、敌人、A*、房门和胜负闭环继续按依赖顺延。

## 11. 日卡顺延分流规则（用户纠正：2026-08-18）

- 原始日卡按日期保存；活动卡由当天原卡加必要的开发补充和课程顺延组成。
- Godot/GDScript课程独立按所属日期顺延：当天未完成，下一张活动卡继续显示当天这组课；后续日期的课程暂不显示。C#已完成课程不重复。
- 开发任务不能丢失，每项未完成开发都作为独立补充项累计，依赖顺序为 `Locomotion/Dodge` → `Health/Hurt/Dead` → 手枪 → 对象池 → 敌人 → A* → 房门 → 胜负闭环。
- 08-13已由用户确认完成；当前课程顺延点是08-14 Godot P25—P29与GDScript P13—P18，完成前不显示08-15及之后课程。
