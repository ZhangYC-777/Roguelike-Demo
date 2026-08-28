# Unity 暑期学习项目｜可迁移项目记忆

> 用途：把本项目的目标、路线、边界、教程来源和教学方式交给另一台电脑上的 Codex。
> 更新时间：2026-08-22（Asia/Shanghai）
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

## 12. 2026-08-22 Health/Hurt/Dead 教学进度

- 用户明确纠正今日课程安排：课程块以其提供的图片为准，执行 Godot P50—P55、GDScript P22—P23、唐老狮 C# 进阶 P38；不要再按旧顺延点给今日课程排课。该纠正目前只适用于用户明确指定的今日安排，后续日卡仍需以用户最新说明和可用计划文件为准。
- 今日 Unity 唯一目标为玩家最小生命闭环：统一伤害入口 → `Health` → `Hurt` → 受击后短暂无敌 → `Dead` → 按 R 重开；固定状态优先级为 `Dead > Hurt > Dodge > Locomotion`。
- A 块课程/概念学习已由用户完成并通过问答确认，参考了地牢枪手 P127、P129—P133 与 Unity State Pattern 的小型 `enum / ChangeState / switch` 思路。用户已能区分：`IDamageable` 只规定统一受伤入口；`Health` 验证伤害、实际扣血并判断死亡；事件负责把已经发生的生命变化或死亡结果通知订阅者。
- 已确认的伤害数据流：子弹或敌人接触只检测命中、提供伤害值并调用 `TakeDamage`；`Health` 在死亡、Dodge 免疫和受击无敌检查通过后才修改生命；有效伤害发布 `HealthChanged`，生命归零发布一次 `Died`。伤害被无敌拒绝时不扣血、不发布事件、也不重置无敌计时，避免持续接触造成“无限续杯”。
- 已确认的事件认知：`Health` 是发布者；`HealthChanged` 可携带当前生命和最大生命；UI、玩家状态处理及其他受伤表现可以分别订阅。同一事件可通知多个订阅者，发布者不直接依赖具体 UI。UI 只显示结果，不管理真实生命。
- 已确认的状态机适配：致命伤害发生时由 `Died` 触发 `ChangeState(Dead)`；进入 `Dead` 后，旧 Dodge/Hurt 计时结果不得把状态切回 `Locomotion`。`HealthChanged` 的玩家监听逻辑看到当前生命为零时不应先进入 Hurt，死亡由 `Died` 处理。
- 当前准确起点为 B1：由用户亲自创建 `Assets/Scripts/Combat` 文件夹和 `IDamageable.cs`。`Combat` 仅表示“战斗”代码分类，不是 Unity 特殊目录；`IDamageable` 应为公开接口，只声明一个无返回值、接收一个整数伤害值的 `TakeDamage` 方法，不继承 `MonoBehaviour`，不写扣血实现。
- B1 尚未开始或完成：截至本次记忆更新，用户只询问了 `Combat` 的含义，尚未提供 `IDamageable.cs` 内容或 Unity 编译证据。新会话必须从 B1 创建并核对接口开始；不要推断 `Combat`、`IDamageable`、`Health`、`Hurt`、`Dead`、短暂无敌或 R 重开已经实现，也不要直接替用户修改脚本。
- 2026-08-22：B1 已由用户完成并通过核验。`Assets/Scripts/Combat/IDamageable.cs` 为公开接口，只声明 `void TakeDamage(int damage)`，未继承 `MonoBehaviour`、未包含扣血实现；Unity MCP 刷新并完成编译后，Console 错误和警告均为 0。下一步进入 B2，只建立 `Health` 的最小数据骨架，尚未实现 Hurt、Dead、短暂无敌或 R 重开。
- 2026-08-22：B2 已由用户完成并通过核验。`Health.cs` 继承 `MonoBehaviour`、实现 `IDamageable`，并提供了签名匹配的空 `TakeDamage(int damage)` 方法；Unity MCP 刷新并完成编译后，Console 错误和警告均为 0。当前尚无生命字段或扣血逻辑，下一步只建立 `maxHealth/currentHealth` 及初始化关系。
- 2026-08-22：B3 部分完成。用户已声明私有 `maxHealth/currentHealth`，并在 `Awake()` 中执行 `currentHealth = maxHealth`；Unity MCP 编译后 Console 错误和警告均为 0。但 `maxHealth` 尚未序列化，Inspector 无法配置，B3 尚未通过。下一步只为 `maxHealth` 增加 Inspector 序列化，不进入扣血逻辑。
- 2026-08-22：B3 已由用户完成并通过核验。`maxHealth` 使用 `[SerializeField] private`，可由 Inspector 配置但仍保持对其他脚本封装；`currentHealth` 为普通私有运行时字段，并在 `Awake()` 中从 `maxHealth` 初始化。Unity MCP 编译后 Console 错误和警告均为 0。尚未进入实际扣血逻辑。
- 2026-08-22：B4 部分完成。`TakeDamage` 已能扣减 `currentHealth` 并在生命小于等于 0 时归零，Unity MCP 编译后 Console 错误和警告均为 0；但入口当前只拒绝 `damage < 0`，尚未拒绝 `damage == 0`。下一步只把入口条件修正为拒绝所有非正伤害，防止后续 0 伤害错误触发事件或无敌计时。
- 2026-08-22：B4 已由用户完成并通过核验。`TakeDamage` 入口会拒绝 `damage <= 0`，有效伤害扣减 `currentHealth`，归零保护保证生命不会成为负数；Unity MCP 编译后 Console 错误和警告均为 0。当前尚未发布 `HealthChanged/Died`，也尚未实现死亡后的重复伤害拒绝。
- 2026-08-22：B5 已由用户完成并通过核验。`Health` 已声明公开事件 `HealthChanged`，类型为 `Action<int, int>`，计划依次携带当前生命与最大生命；Unity MCP 编译后 Console 错误和警告均为 0。事件目前仅声明、尚未在有效伤害后发布。
- 2026-08-22：B6 已由用户完成并通过核验。`TakeDamage` 在有效扣血与归零保护完成后调用 `HealthChanged?.Invoke(currentHealth, maxHealth)`；非正伤害已在调用前返回，因此每次有效伤害只发布一次。Unity MCP 编译后 Console 错误和警告均为 0。下一边界是生命已经归零后仍会接受正伤害并重复发布变化事件。
- 2026-08-22：B7 已由用户完成并通过核验。`TakeDamage` 入口现同时拒绝 `damage <= 0` 与 `currentHealth <= 0`，保证生命归零后不再扣血或重复发布 `HealthChanged`；Unity MCP 编译后 Console 错误和警告均为 0。下一步开始建立单次 `Died` 通知。
- 2026-08-22：B8 已由用户完成并通过核验。`Health` 已在成员区域声明公开、无参数的 `Action Died` 事件；Unity MCP 编译后 Console 错误和警告均为 0。事件尚未发布。
- 2026-08-22：B9 已由用户完成并通过核验。有效伤害会先发布 `HealthChanged`，随后在 `currentHealth <= 0` 时发布 `Died`；由于入口已拒绝死亡后的伤害，`Died` 在当前伤害入口下只会发布一次。Unity MCP 编译后 Console 错误和警告均为 0。尚未将 `Health` 挂到玩家 Prefab，也没有运行时伤害调用证据。
- 2026-08-22：B10 已由用户完成并通过核验。Unity MCP 确认 `Assets/Prefabs/Player/Player.prefab` 根对象已包含 `Health` 组件，Prefab 序列化数据确认 `maxHealth: 100`，Console 错误和警告均为 0。尚未进行运行时扣血与事件次数验证。
- 2026-08-22：B11 运行验证通过。用户进入 Play 后，Unity MCP 对运行时 `Player(Clone)` 的 `Health` 做了不保存到工程的测试：初始 100，受到 30 伤害后为 70，足量致死伤害后为 0，死亡后再次伤害仍为 0；事件次数为 `HealthChanged=2`、`Died=1`，顺序为 `HealthChanged(70/100) → HealthChanged(0/100) → Died`。基础生命扣减、归零保护、有效伤害通知和单次死亡通知均已有运行证据。当前 Play 会话中的测试玩家生命为 0，退出 Play 后会恢复 Prefab 配置。
- 2026-08-22：用户已退出 B11 的 Play 测试，Unity MCP 确认编辑器未播放、未切换且空闲。下一阶段开始把 `HealthChanged/Died` 接入现有玩家 FSM；当前 `PlayerState` 仍只有 `Locomotion/Dodge`，尚无 `Hurt/Dead`。
- 2026-08-22：B12 已由用户完成并通过核验。现有 `PlayerState` 已扩充为 `Locomotion、Dodge、Hurt、Dead`，尚未添加新状态行为或生命事件订阅；Unity MCP 编译后 Console 错误和警告均为 0。
- 2026-08-22：B13 首次核验未推进。用户表示理解并完成，但磁盘中的 `PlayerMove.cs` 尚无私有 `Health` 引用，也无用于 `GetComponent<Health>()` 的 `Awake()`；可能是尚未编写或编辑器内容未保存。不得进入 `OnEnable/OnDisable` 订阅，下一步仍是完成并保存 B13 后复核。
- 2026-08-22：B13 后续确认只是脚本未保存；保存后已通过核验。`PlayerMove` 现有私有 `Health playerHealth`，并在 `Awake()` 中通过 `GetComponent<Health>()` 获取同对象组件；Unity MCP 编译后 Console 错误和警告均为 0。尚未订阅任何生命事件。
- 2026-08-22：B14 部分完成。用户已在 `PlayerMove` 中创建签名匹配的空处理方法 `ChangeHealth(int currentHealth, int maxHealth)` 与 `Die()`，Unity MCP 编译后 Console 错误和警告均为 0；但尚无 `OnEnable/OnDisable`，因此 `HealthChanged/Died` 实际尚未订阅或退订。
- 2026-08-22：B14 已由用户完成并通过核验。`PlayerMove` 在 `OnEnable()` 中分别用 `ChangeHealth`、`Die` 订阅 `HealthChanged/Died`，并在 `OnDisable()` 中成对退订；两个处理方法签名匹配且方法体仍为空。Unity MCP 编译后 Console 错误和警告均为 0。尚未产生状态切换行为。
- 2026-08-22：B15 已由用户完成并通过核验。`ChangeHealth` 在事件传入的当前生命小于等于 0 时直接返回，非致命生命变化才调用 `ChangeState(PlayerState.Hurt)`，避免致命伤害先进入 Hurt；Unity MCP 编译后 Console 错误和警告均为 0。`Die()` 仍为空，Hurt 也尚无物理行为或退出条件，因此暂不进行 Play 测试。
- 2026-08-23：B16 已由用户完成并通过核验。`Die()` 现在通过统一入口调用 `ChangeState(PlayerState.Dead)`；Unity MCP 刷新编译后 Console 错误和警告均为 0。当前 `Hurt/Dead` 尚未在 `FixedUpdate()` 中定义物理行为，`Hurt` 也没有退出计时，因此仍不进行 Play 验收。下一步先建立 Hurt 持续时间与计时器字段，不提前处理死亡动画或 R 重开。
- 2026-08-23：B17 部分完成。用户已声明公开的受伤持续时间字段与私有 `hurtTimer`，Unity MCP 编译后 Console 错误和警告均为 0；但持续时间字段当前误写为 `hurtDuartion`。下一步只将其更正为 `hurtDuration`，更正并复核前不接入 Hurt 计时。
- 2026-08-23：B17 已由用户完成并通过核验。受伤持续时间字段已更正为公开的 `hurtDuration`，倒计时字段为私有 `hurtTimer`；Unity MCP 刷新编译后 Console 错误和警告均为 0。两个字段尚未接入状态进入或逐帧计时逻辑。
- 2026-08-23：B18 已由用户完成并通过核验。`ChangeState()` 在新状态为 `Hurt` 时将 `hurtTimer` 重置为 `hurtDuration`，使计时器初始化归属于状态进入时刻；Unity MCP 刷新编译后 Console 错误和警告均为 0。当前 `FixedUpdate()` 尚无 Hurt 分支，倒计时不会递减，玩家也不会自动返回 Locomotion。
- 2026-08-23：B19 代码已通过核验。`FixedUpdate()` 的 `Hurt` 分支会将 Rigidbody2D 速度归零、用 `Time.fixedDeltaTime` 递减 `hurtTimer`，并在归零后通过 `ChangeState(Locomotion)` 退出；Unity MCP 编译后 Console 错误和警告均为 0。临时 Play 测试向 `Player(Clone)` 注入 1 点伤害时发现运行时 `hurtDuration=0`，因此 Hurt 会在首个物理帧立即结束，尚不能作为短暂受伤闭环的运行验收。已退出 Play；下一步只在 Player Prefab 上将 Hurt Duration 配置为正值后复测。
- 2026-08-23：B19 配置复核未通过。用户反馈已将 Hurt Duration 设为 0.2，但磁盘中的 `Assets/Prefabs/Player/Player.prefab` 尚无 `hurtDuration` 序列化字段，重新进入 Play 后 `Player(Clone).hurtDuration` 仍为 0。已退出 Play，本轮未推进运行验收。下一步需在非 Play 模式打开 Player Prefab 本体、设置并保存 Hurt Duration，再复核磁盘与运行时值。
- 2026-08-23：用户澄清其修改是在代码中将字段初始化为 `public float hurtDuration = 0.2f`，代码核验确认无误。Codex 强制刷新并重新编译后再次 Play，运行时 `Player(Clone).hurtDuration` 仍为 0；原因是该 public 序列化字段在已有 Player Prefab 上已保存为 0，之后修改代码初始化值不会覆盖已有序列化值。已退出 Play。下一步仍只需在非 Play 模式把 Player Prefab 本体的 Hurt Duration 改为 0.2 并保存；无需再次修改计时代码。
- 2026-08-23：Player Prefab 的 `hurtDuration: 0.2` 已写入磁盘并通过运行时核验。向 `Player(Clone)` 临时注入 1 点伤害后，状态立即为 `Hurt`、Rigidbody2D 速度为零。因 Unity 窗口失焦且项目未在后台运行，自动帧数停在 2；使用不保存的运行时反射连续执行 11 个 `FixedUpdate` 物理步后，`hurtTimer` 从 0.2 降至 -0.01999997，状态正确返回 `Locomotion`，速度仍为零，Console 无错误或警告。已退出 Play。B19 的 Hurt 最小计时闭环通过逻辑与运行时步进核验；尚未加入受击无敌或动画。
- 2026-08-23：B20 已由用户完成并通过核验。`FixedUpdate()` 新增 `Dead` 独立分支，只将 Rigidbody2D 速度设为零，没有计时或退出入口；Unity MCP 编译后 Console 错误和警告均为 0。临时 Play 中注入致死伤害并执行 20 个物理步后，玩家仍为 `Dead`、速度为零，验证旧 Hurt/Dodge 计时不会把死亡状态拉回 Locomotion。已退出 Play。下一步开始短暂无敌，尚不处理死亡动画或 R 重开。
- 2026-08-23：B21 已由用户完成并通过核验。`Health` 已声明 `[SerializeField] private float invincibilityDuration = 0.5f` 与完全私有的 `invincibilityTimer`，字段封装和职责正确；Unity MCP 刷新编译后 Console 错误和警告均为 0。当前计时器尚未递减、伤害入口尚未检查无敌、有效伤害后也尚未启动计时。
- 2026-08-23：B22 已由用户完成并通过核验。`Health.Update()` 仅在 `invincibilityTimer > 0` 时使用 `Time.deltaTime` 递减无敌计时器；Unity MCP 刷新编译后 Console 错误和警告均为 0。计时器仍未在有效伤害后启动，伤害入口也尚未检查无敌状态。
- 2026-08-23：B23 已由用户完成并通过核验。`TakeDamage()` 的统一入口条件已同时拒绝 `damage <= 0`、`currentHealth <= 0` 与 `invincibilityTimer > 0`；无敌拒绝发生在扣血和事件发布之前，因此被拒绝的伤害不会扣血或发布事件。Unity MCP 刷新编译后 Console 错误和警告均为 0。当前有效伤害后尚未给 `invincibilityTimer` 赋值，所以无敌窗口仍不会实际启动。
- 2026-08-23：B24 已由用户完成并通过核验。有效伤害扣血后、事件发布前会将 `invincibilityTimer` 重置为 `invincibilityDuration`；运行时确认持续时间为 0.5。临时测试结果：首次 10 点伤害使生命 100→90 且计时为 0.5，窗口内立即再次伤害生命仍为 90 且计时保持 0.5，临时将计时置零代表窗口结束后第三次伤害使生命 90→80 并重新启动 0.5。Console 无错误或警告，已退出 Play。短暂无敌闭环通过；下一步接 Dodge 状态免伤。
- 2026-08-23：B25 已由用户完成并通过核验。`Health` 已增加完全私有的 `isDamageImmune` 与公开 `SetDamageImmune(bool isImmune)`；公开方法只负责给私有字段赋值，未让 Health 依赖 PlayerState。Unity MCP 刷新编译后 Console 错误和警告均为 0。当前 `TakeDamage()` 尚未检查该字段，PlayerMove 也尚未在 Dodge 进入/退出时调用开关。
- 2026-08-23：B26 已由用户完成并通过核验。`TakeDamage()` 的统一入口已加入 `isDamageImmune`，状态免伤期间会在扣血、短暂无敌计时重置和事件发布之前直接返回；Unity MCP 刷新编译后 Console 错误和警告均为 0。PlayerMove 尚未根据 Dodge 状态切换该开关。
- 2026-08-23：B27 部分完成。用户按自己的思路在 `ChangeState(Dodge)` 分支调用 `playerHealth.SetDamageImmune(true)`，能在进入翻滚时开启免伤；但离开 Dodge 时没有任何路径将其关闭，因此首次翻滚后会永久免伤。下一步只把免伤同步集中到 `ChangeState()`：用“新状态是否为 Dodge”的布尔比较统一赋值，并移除 Dodge 分支中的重复开启调用；修复前不做运行验收。
- 2026-08-23：B27 已由用户修改为集中同步写法并通过核验。`ChangeState()` 更新状态后统一调用 `playerHealth.SetDamageImmune(newState == PlayerState.Dodge)`，原先分散在 Dodge 进入/计时退出处的 true/false 调用已移除。Unity MCP 编译无错误或警告；运行时确认 Dodge 中免伤为 true、切回 Locomotion 后为 false，Dodge 中 10 点伤害被拒绝（生命保持 100），退出后同样伤害生效（生命 100→90）。已退出 Play。Dodge 免伤闭环完成。
- 2026-08-23：B28 代码已通过核验。`PlayerMove` 已引入 `UnityEngine.SceneManagement`，并在 `Update()` 中仅当 `currentState == Dead` 且本帧按下 R 时，使用当前活动场景的 buildIndex 调用 `SceneManager.LoadScene`；Unity MCP 编译后 Console 错误和警告均为 0。当前 Play 会话已通过临时致死伤害将玩家置为 `Dead`、生命 0，等待用户在 Unity Game 窗口真实按 R，验证场景重载与满血玩家重新生成；此运行验收尚未完成。
- 2026-08-23：B28 真实运行验收通过。用户在 `Dead`、生命 0 时于 Unity Game 窗口按 R，确认场景成功重载；Unity MCP 复核新玩家为 `Locomotion`、生命 100、Rigidbody2D 速度为零，Console 无错误或警告，随后已退出 Play。
- 2026-08-23：玩家 `Health/Hurt/Dead` 开发闸门完成。已有证据覆盖统一 `IDamageable.TakeDamage`、生命扣减与归零保护、`HealthChanged`、单次 `Died`、非致命伤害进入并定时退出 Hurt、有效伤害后的 0.5 秒短暂无敌、Dodge 状态免伤、Dead 停止且不会被旧计时器拉回、死亡后 R 重载当前场景。受击/死亡动画和失败 UI 尚未制作，不影响本轮最小生命闭环通过；下一开发闸门按固定依赖进入武器主链。

## 13. 2026-08-24 手枪武器主链教学进度

- 用户将教学粒度从“每次一句一个任务”调整为“每次用 2—3 句讲清一个连贯小任务”；仍保持一次只推进一个可验证逻辑块，由用户亲自实现并反馈运行证据。
- 今日开始前已核验：玩家 `Locomotion/Dodge` 与 `Health/Hurt/Dead` 闸门均已通过；Player Prefab 原有 `WeaponRotationPoint → WeaponAnchorPosition → WeaponShootPosition` 挂点链及手枪/子弹素材，但没有武器、投射物脚本或正式子弹 Prefab。
- A 块已完成。用户创建 `Assets/Scripts/Weapon/WeaponDefinition.cs`，以 `ScriptableObject` 保存武器名称、子弹 Prefab、伤害、子弹速度、存活时间和射击间隔；字段使用 `[SerializeField] private`，并分别提供公开只读属性。`CreateAssetMenu` 路径为 `Weapons/Weapon Definition`。
- 用户创建 `Assets/WeaponDefinitions/PistolDefinition.asset`，配置为 `Pistol`、伤害 25、子弹速度 12、存活时间 2 秒、射击间隔 0.3 秒，并在后续正确引用 `PistolProjectile.prefab`。
- 用户创建 `WeaponController.cs` 并挂到 Player Prefab 的 `WeaponRotationPoint`；组件正确引用 `PistolDefinition` 与 `WeaponShootPosition`。控制器使用鼠标左键单发、`Time.time/nextFireTime` 射速门控、临时 `Instantiate` 生成子弹，并以 `shootPoint.right` 作为鼠标瞄准方向。
- 用户创建 `Assets/Prefabs/Projectiles/PistolProjectile.prefab`：Layer 为 `PlayerAmmo`，Sorting Layer 为 `Instances`，含 SpriteRenderer、Gravity Scale 0 的 Dynamic Rigidbody2D、Continuous 碰撞检测和 Is Trigger 的 CircleCollider2D，并挂载 `Projectile`。
- `Projectile.Initialize` 会接收方向、伤害、速度与存活时间；保存 25 点伤害快照，以单位化方向乘速度设置 Rigidbody2D velocity，并使用临时 `Destroy(gameObject, lifetime)` 完成 2 秒超时结束。后续对象池闸门再替换 `Instantiate/Destroy`，当前不提前池化。
- Physics 2D Layer Collision Matrix 已关闭 `PlayerAmmo × Player` 和 `PlayerAmmo × PlayerAmmo`，保留 `PlayerAmmo × Wall/Enemy`；防止玩家自己的子弹命中自身和子弹互撞。
- `Projectile.OnTriggerEnter2D` 使用 `GetComponentInParent<IDamageable>()` 查找统一伤害入口；找到时调用 `TakeDamage(damage)`，随后无论墙体还是可受伤目标均销毁子弹。用户运行确认墙体会让子弹立即消失；临时 100 HP Enemy Layer 目标运行确认单发伤害后生命为 75，临时测试日志与目标均未保留。
- `PlayerMove` 新增实时计算的公开只读属性 `CanFire => currentState == PlayerState.Locomotion`；`WeaponController` 通过 `GetComponentInParent<PlayerMove>()` 获取玩家，并将 `playerMove.CanFire` 加入开火条件，因此 Dodge、Hurt、Dead 均禁射。
- 今日 C 块运行验收已由用户确认通过：普通状态左键按 0.3 秒配置单发；子弹上下左右均沿鼠标/枪口方向正确飞行；墙体命中立即结束；`IDamageable` 目标受到 25 点伤害后子弹结束；未命中时约 2 秒超时结束；Dodge 期间连续点击不生成子弹、回到 Locomotion 后立即恢复；Console 无阻断性红色报错。
- 今日武器主链开发闸门完成。尚未处理对象池、霰弹枪、切枪、弹药、音效、枪口效果或后坐力；下一固定开发闸门为两把枪共用的 `ObjectPool<Projectile>`，但开始前应先由用户决定是否结算/提交今天的手枪主链变更，Codex 不自动提交 Git。

## 14. 2026-08-25 Projectile 对象池教学进度

- 用户先完成对象池概念学习，已能解释 `Get/Release`、命中敌人/撞墙/超时三个归还原因、重复归还保护，以及 `ObjectPool<Projectile>` 与 `IObjectPool<Projectile>` 引用同一实例的接口关系。
- `WeaponController` 已持有并在 `Awake()` 创建 `ObjectPool<Projectile>`；创建、取出、归还、池满销毁回调分别负责首次实例化、激活、停用与真正销毁，`collectionCheck` 已开启，默认容量为 10、最大容量为 50。
- `Fire()` 已由逐枪 `Instantiate` 改为 `projectilePool.Get()`，每次取出后重设枪口位置和旋转，再调用现有 `Initialize` 写入本轮方向、伤害、速度与寿命。普通发射链路不再直接创建子弹；`Instantiate` 仅保留在池的创建回调中。
- `Projectile` 通过 `IObjectPool<Projectile>` 保存创建它的池引用；`CreateProjectile()` 创建组件后使用 `SetProjectilePool` 注入同一个池。子弹不创建或查找自己的池。
- `Projectile.Initialize()` 每轮复位 `hasReturned=false` 和 `remainingLifetime`；旧的两个普通 `Destroy` 已移除。`Update()` 在寿命耗尽时调用 `TryReturnToPool()`；`OnTriggerEnter2D()` 保持先调用 `IDamageable.TakeDamage`，随后无论敌人还是墙都调用同一个 `TryReturnToPool()`。
- `TryReturnToPool()` 会先拒绝已经归还的本轮请求，再标记 `hasReturned=true`、清零 Rigidbody2D 速度并调用 `Release(this)`；因此三个结束原因共享同一防重复门。真正的 `Destroy` 只保留在池达到最大容量时的销毁回调中。
- 用户运行确认：未命中子弹约 2 秒后在 Hierarchy 变灰但不消失，下一枪复用同一 Clone；撞墙会立即归还且再次射击不增加 Clone；命中实现 `IDamageable` 的测试目标后伤害与归还路径正常；连续发射 20—30 枪后 Clone 总数趋于稳定，停止射击后全部停用，Console 无重复 `Release` 或其他红色错误。
- 用户有意保留 `Assets/Prefabs/TestEnemy/enemy.prefab` 作为长期回归夹具；当前配置为 Enemy Layer、SpriteRenderer、BoxCollider2D 与 100 HP `Health`，无敌人 AI 或额外逻辑，应作为本轮新增测试资产保留。
- 当前对象池闸门已完成“手枪真实池化、三入口统一归还、防重复 Release、数量稳定”的代码与运行验收。工程尚无霰弹枪、切枪或第二个武器定义，因此“两把枪实际共用同一池”目前只有 `WeaponController` 单池架构条件，尚无双枪运行证据；后续建立霰弹枪时需复用同一基础 Projectile Prefab 并补做双枪共池验收。
- 本轮尚未提交 Git；功能改动位于 `WeaponController.cs`、`Projectile.cs`，新增长期测试 Prefab 位于 `Assets/Prefabs/TestEnemy/`。

## 15. 2026-08-26 霰弹枪与切枪教学进度

- 用户调整教学节奏：以后先由 Codex 结合课程讲清当前功能的整体概念与数据流，再进入分块学习和实际开发；仍由用户亲自实现，Codex 负责核验和下一步教学。
- `WeaponDefinition` 已新增私有序列化字段 `projectileCount`、`spreadAngle` 及对应公开只读属性 `ProjectileCount`、`SpreadAngle`。`PistolDefinition` 已保存为弹丸数量 1、散射角 0。
- 已创建 `ShotgunDefinition`，配置为名称 `Shotgun`、伤害 10、弹速 10、寿命 1 秒、射击间隔 0.8 秒、弹丸数量 5、总散射角 30；与手枪引用同一个 `PistolProjectile.prefab`。
- `WeaponController` 已将单个武器定义改为 `WeaponDefinition[] weaponDefinitions`，以 `currentWeaponIndex` 和只读 `CurrentWeaponDefinition` 统一读取当前武器。Player Prefab 数组顺序已核验为 0 手枪、1 霰弹枪；对象池仍只有 `WeaponController.Awake()` 创建的同一个实例。
- 1/2 切枪已接入：主键盘 1/2 分别请求索引 0/1，`SwitchWeapon` 拒绝越界与重复选择，真正切换后重置 `nextFireTime` 以允许新武器立即开火；切枪处理位于开火判断之前。Unity 编译无错误或警告，用户运行确认可以稳定切换、无异常。
- 当前霰弹枪仍只生成一颗弹丸。下一准确教学起点是只让 `Fire()` 按当前武器的 `ProjectileCount` 多次从同一池取弹并初始化，先验证数量，再加入散射方向。
- `Fire()` 已按 `CurrentWeaponDefinition.ProjectileCount` 循环执行取弹、定位、旋转和初始化，射击冷却仍只在一次 `Fire()` 后更新一次。Unity 编译无错误或警告；用户运行确认手枪一次有 1 个活动弹丸、霰弹枪一次有 5 个活动弹丸，五颗霰弹当前方向相同并完全重叠。下一步加入以枪口方向为中心的对称散射。
- 对称散射已完成并通过运行验收。`Fire()` 对单弹丸使用 0 起始角和 0 间隔，对多弹丸使用 `-SpreadAngle/2` 起点与 `SpreadAngle/(ProjectileCount-1)` 间隔；每颗弹丸都以 `Quaternion.Euler` 从原始 `shootPoint.right` 得到独立 `rotatedDirection` 并传入 `Initialize()`。用户确认手枪朝四向均沿鼠标中心发射、霰弹枪五颗形成以枪口为中心的对称扇形、切回手枪不继承旧偏移，视觉表现也合理。当前不额外修改弹丸 Transform 朝向；下一步进行射速差异与双枪共池数量稳定的综合验收。
- 双枪共池综合运行验收已由用户确认通过：1/2 切换稳定，手枪保持单发，霰弹枪每次五颗并呈 30 度对称散射，两把枪射速差异生效；霰弹枪归还后的对象可供手枪继续复用，相同压力重复射击时 Projectile Clone 总量不持续增长，停止射击后弹丸均会停用。Unity MCP 复核编辑器已退出 Play、当前空闲，Console 错误和警告均为 0。随后发现切枪只改变数据，Player Prefab 的 `Weapon` 子对象 `SpriteRenderer` 仍固定引用 `Pistol.png`，`WeaponDefinition` 也尚无枪械 Sprite 字段，因此霰弹枪图片不会切换。今日目标暂不结算；下一步先给武器定义增加 Sprite 数据，再由 `WeaponController` 在初始和切换时同步 `Weapon` 的 SpriteRenderer，最后移除临时日志并编译确认。
- 武器图片切换已补齐并通过运行回归。`WeaponDefinition` 新增私有序列化 `weaponSprite` 与公开只读 `WeaponSprite`，两份定义分别引用 `Pistol.png` 与 `Shotgun.png`；`WeaponController` 持有 Player Prefab 中 `Weapon` 子对象的 `SpriteRenderer`，通过 `ApplyCurrentWeaponVisual()` 在 `Awake()` 初始同步，并在真正更新武器索引后同步新图片。用户确认进入 Play 初始显示手枪，1/2 切换时图片、单发/散射、鼠标朝向、枪口位置与共池复用均正常。临时切枪日志已移除，Unity 最终刷新编译后 Console 错误和警告均为 0，编辑器已退出 Play且空闲。2026-08-26 今日唯一目标“霰弹枪、多弹丸散射、1/2 切枪、两把枪共用同一对象池且池对象数量不持续增长”正式完成；改动尚未提交 Git。

## 16. 2026-08-27 SlimeBlock Dormant/Chase 教学进度

- 今日唯一目标为建立正式 SlimeBlock 普通敌人 Prefab，并完成 `Dormant（休眠）→ Chase（追踪）` 激活、直线追踪、停止距离和实体碰撞边界；今天明确不实现 A*、Boss、Attack 或 Dead，也不叠加 C# P38/P40 之后的课程。
- 已创建 `Assets/Prefabs/Enemies/SlimeBlock.prefab`。磁盘核验确认根对象位于 Enemy Layer，包含 SpriteRenderer、Animator、Dynamic Rigidbody2D 和非 Trigger BoxCollider2D；Animator 绑定普通敌人的 `SlimeBlock.controller`，关闭 Root Motion、Update Mode 为 Normal；Rigidbody2D 的 Gravity Scale 为 0、Interpolate 为 Interpolate、Collision Detection 为 Continuous，并冻结 Z 轴旋转。
- 用户已在 Play 模式手动验证 Animator 参数链：`isIdle=true/isMoving=false/aimDown=true` 时 `IdleDown` 正确高亮；`isIdle=false/isMoving=true/aimDown=true` 时 `MoveDown` 正确高亮，用户反馈无问题。当前素材、Controller 与 Idle/Move 过渡接线通过。
- 尚未创建正式敌人控制脚本，尚无玩家目标引用、状态枚举、激活条件、追踪移动或停止距离运行证据。下一准确教学起点是建立敌人脚本骨架，并安全获取由 `PlayerSpwaner` 在运行时生成的 Player Transform；不得从计划推断 `Dormant/Chase` 已实现。
- 2026-08-27：敌人脚本骨架部分完成。用户已创建 `Assets/Scripts/Enemy/EnemyController.cs`，包含 `Dormant/Chase` 枚举、自身 Rigidbody2D/Animator 缓存、初始速度归零、`isIdle/isMoving/aimDown` 初始动画参数，以及仅在 `playerTarget == null` 时调用的查找入口。当前未通过目标引用验收：`TryFindPlayer()` 直接对 `FindGameObjectWithTag("Player")` 的返回值访问 `.transform`，若敌人先于运行时玩家生成会在空值判断之前触发 `NullReferenceException`；同时磁盘核验显示该脚本尚未挂载到场景 SlimeBlock 或正式 `SlimeBlock.prefab`。下一步只修正查找顺序并将组件保存到正式 Prefab，再进行运行验证，不进入激活距离。
- 2026-08-27：玩家查找的空引用顺序已由用户修正。当前实现先用 `GameObject.FindGameObjectWithTag("Player")` 保存对象，确认非空后才读取 `.transform`，因此玩家尚未生成时不会再因提前访问 Transform 抛出空引用。磁盘复核仍未在正式 `SlimeBlock.prefab` 或当前场景 SlimeBlock 对象中发现 `EnemyController` 组件；`currentState` 与目标引用也未序列化显示，三个配置值目前改成了 public 字段而非项目惯用的 `[SerializeField] private`。本步继续保持“部分完成”，下一步先修正字段封装、去除重复目标引用并把脚本保存到正式 Prefab，再进行 Play 验收。
- 2026-08-27：最新磁盘核验确认 `EnemyController` 已保存到正式 `SlimeBlock.prefab`，Prefab 中的移动速度、激活距离和停止距离分别为 2、5、1.2。当前 `TryFindPlayer()` 使用局部 `GameObject playerTarget`，但控制流仍未通过：第一次查找成功时立即记录日志并 `return`，没有把 `playerTarget.transform` 赋给字段 `playerTransform`；赋值只存在于第一次查找失败后的重复第二次查找分支。因此正常情况下 `playerTransform` 会始终为空、`Update()` 每帧重复查找并输出成功日志。下一步只修正这一方法的判空守卫与赋值顺序，再进行运行验收。
- 2026-08-27：`TryFindPlayer()` 的控制流已由用户修正并通过静态核验。当前每次只调用一次 `FindGameObjectWithTag("Player")`；找到时先把目标 Transform 保存到 `playerTransform`，随后返回；未找到时保持字段为空，使 `Update()` 后续继续尝试。正式 Prefab 仍正确挂载 `EnemyController`，配置值仍为移动速度 2、激活距离 5、停止距离 1.2。当前仅缺 Play 运行证据：确认成功日志只出现一次、敌人保持 Dormant/IdleDown 且 Console 无空引用；运行验收前不进入激活距离。
- 2026-08-27：玩家目标引用运行验收通过。用户确认 Play 中“玩家已找到”只出现一次，说明目标成功缓存且后续不再重复查找；SlimeBlock 保持 `Dormant` 静止并播放 IdleDown，Console 无 `NullReferenceException`。至此正式敌人 Prefab、普通 SlimeBlock Animator 与运行时玩家目标引用小闸门完成。下一步进入激活状态块：只实现距离判断与 `Dormant → Chase` 状态切换并观察单次日志，暂不让 Chase 产生移动。
- 2026-08-27：用户已自行扩展到激活、Chase 直线移动与停止距离，但当前静态核验未通过。`Dormant` 能按距离请求进入 `Chase`，并已改用 Rigidbody2D `MovePosition`，但移动表达式把 `enemyRigidbody.position` 与 `Vector2.MoveTowards(...)` 返回的绝对下一位置相加，导致当前坐标被重复累加；同时 Chase 分支在检查停止距离之前先调用 MovePosition，进入停止范围仍会先移动一次。Dormant 激活条件也仍额外要求距离大于停止距离。下一步只修正 MoveTowards 返回值用法，并把距离判断放在移动命令之前；修复和运行验证前不视为直线追踪完成。
- 2026-08-27：第二次静态复核确认两项已修正：Dormant 激活现在只检查 `distance <= activationDistance`；MoveTowards 返回的绝对下一位置现在直接传给 Rigidbody2D.MovePosition，不再重复累加当前位置。仍有一个阻断点：MovePosition 位于停止距离 `if` 之后且不在任何分支内，因此即使 `distance <= stopDistance` 已设置零速度和 Idle 动画，本帧仍会无条件发出移动命令。下一步将移动命令和 Moving 动画放入“距离大于停止距离”的分支，停止分支不调用 MovePosition，再进行运行验证。
- 2026-08-27：Chase 移动/停止分支已由用户修正并通过静态核验。当前 Chase 每个物理帧先计算玩家距离；`distance <= stopDistance` 时速度归零并设置 Idle，且不调用 MovePosition；距离更大时才使用 `Vector2.MoveTowards` 计算绝对下一位置、通过 Rigidbody2D.MovePosition 应用，并设置 Moving。状态保持 Chase，激活条件仅为进入 activationDistance。代码层面的 `Dormant → Chase → 直线追踪 → 停止距离` 链已成立，当前缺综合 Play 证据与墙体碰撞边界验证；运行验收前不视为今日闸门完成。
- 2026-08-27：综合 Play 验收由用户确认“没问题”。证据覆盖：玩家在 5 单位激活范围外时 SlimeBlock 保持 Dormant/Idle 且不行动；进入范围后只切换一次 Chase 并沿直线追踪；进入 1.2 单位停止距离后保持 Chase 但停止并播放 Idle；玩家重新拉开后无需再次激活即可继续追踪；墙位于中间时敌人被实体碰撞阻挡、不会穿墙也不会绕路；Console 无阻断性红色错误。今日唯一目标“正式 SlimeBlock Prefab + Animator/运行时玩家引用 + Dormant→Chase 激活 + 直线追踪 + 停止距离 + 碰撞边界”正式完成。六方向动画同步、Attack、Dead、接触伤害与四方向 A* 均未实现且不在今日继续推进。

## 17. 2026-08-28 SlimeBlock Health/Dead 教学进度

- 今日目标为让敌人复用现有 `IDamageable/Health`，随后完成攻击距离、攻击冷却、玩家受伤与 `Dormant/Chase/Attack/Dead` 四状态闭环；B/C 不看视频，依据现有代码实现。
- A 块已把现有 `Health` 挂到正式 `Assets/Prefabs/Enemies/SlimeBlock.prefab` 根对象，配置为 50 点最大生命、0 秒受击无敌。设为 0 是为了不阻挡同一轮霰弹枪的多颗弹丸；Unity 编译无错误或警告。
- `EnemyController` 已用私有 `enemyHealth` 在 `Awake()` 获取同对象 `Health`；`EnemyState` 已加入 `Dead`，私有 `Die()` 通过统一 `ChangeState(Dead)` 进入死亡状态；`OnEnable/OnDisable` 已成对订阅与退订 `enemyHealth.Died`。
- 用户完成 Play 最小死亡测试并反馈“没问题”：50 点生命的 SlimeBlock 被两发 25 点手枪伤害击杀，`Dead` 状态日志只出现一次，后续射击不重复发布死亡，死亡后不再移动，Console 无红色错误。当前死亡停止主要来自 `Dead` 空分支不再发出移动命令，尚需在进入 `Dead` 时主动清零刚体速度并统一设置 Idle，之后再进入 `Attack`。
- `EnemyState.Dead` 的物理分支已补齐确定性停止：每个物理帧将 Rigidbody2D 速度归零，并统一设置 `isMoving=false`、`isIdle=true`；Unity 编译无错误或警告。A 块“复用现有 `IDamageable/Health`、单次 `Died`、Enemy Dead 与死亡停止行动”完成，下一准确起点为 B 块建立 `Attack` 状态和攻击配置数据，尚未实现玩家受伤或攻击冷却。
- B 块攻击骨架已建立：`EnemyState` 顺序为 `Dormant/Chase/Attack/Dead`；序列化配置为攻击距离 1.2、攻击冷却 1 秒、攻击伤害 10，并有完全私有的运行时冷却计时器。`Update()` 仅在计时器大于零时用 `Time.deltaTime` 递减。
- `Chase` 已将旧停止距离判断替换为攻击距离判断：进入 1.2 单位时先停止刚体、切为 Idle，再通过 `ChangeState(Attack)` 进入攻击状态。用户完成 Play 单向转换测试并反馈完成，证据覆盖 `Dormant → Chase → Attack` 依次发生、Attack 只记录一次且保持停止；Unity Console 无错误或警告。当前尚无 `Attack` 分支，因此玩家离开后不会恢复 Chase；下一准确起点只实现 `Attack → Chase` 距离退出。
- `Attack` 物理分支已补齐：玩家离开 1.2 单位时通过 `ChangeState(Chase)` 恢复追踪，仍在范围内时持续将 Rigidbody2D 速度归零并保持 Idle。用户完成 Play 边界往返测试并反馈“没问题”，`Chase → Attack → Chase → Attack` 可重复发生、近距离停止、拉开后恢复且状态不锁死；Console 无错误或警告。case 的书写顺序不作为运行正确性的条件。当前尚未缓存玩家 `IDamageable`，也尚未实际扣血或重置攻击冷却。
- 玩家 `IDamageable` 已由敌人在找到运行时玩家时缓存；`Attack` 范围内仅在冷却小于等于零时调用 `TakeDamage(10)`，随后把计时器重置为 1 秒，攻击判断位于距离范围分支内部，避免离开范围的同一帧补伤害。用户指出首次进入 Attack 会因计时器初始为零而立即扣血、缺少起手提示；已明确将“起手等待/攻击动画命中帧”留作后续手感优化，今天暂不实现，不阻塞原型闭环。当前等待玩家运行验证 Hurt、约 1 秒攻击节奏及离开范围停止伤害。
- 玩家受伤与攻击冷却 Play 验收通过。用户反馈“没问题”，证据覆盖进入范围后玩家进入 Hurt、留在范围内约每 1 秒受到一次 10 点伤害而非逐帧扣血、离开 1.2 单位后停止伤害并恢复 Chase；Unity Console 无错误或警告。下一步只验证敌人持续攻击能否将玩家推进现有 `Dead` 链，暂未制作失败提示 UI。
- 敌人持续攻击驱动玩家死亡的 Play 验收通过。用户确认玩家生命归零后 `Dead` 只出现一次，死亡后不能移动或开火，继续留在攻击范围也不会再次进入 Hurt 或重复发布 Dead；用户还提前确认按住 R 可以成功重载当前场景。Unity Console 无错误或警告。B 块“攻击距离、攻击冷却、玩家受伤、Enemy Dead、死亡后停止行动”已具备代码与运行证据；C 块剩余主要工作为失败提示 UI 与四状态/玩家无敌/死亡/重开的综合回归。当前场景和现有 Prefab/脚本中未检索到 Canvas、TMP 或失败提示结构。
- C 块已在 `SampleScene` 建立场景级 `GameOverUICanvas`，Canvas 保持激活、Screen Space Overlay、Sorting Order 100；其子 `Panel` 默认不激活，包含 TMP 失败文字。已创建并挂载 `GameOverUI`，序列化引用正确指向隐藏 Panel。
- `GameOverUI` 会仅在 `playerHealth` 为空时按 Player Tag 重试查找运行时生成的玩家，找到后缓存根对象 `Health` 并停止重复查找。用户完成 Play 查找测试并反馈“没问题”：找到日志只出现一次、Panel 仍隐藏、Console 无红色错误。当前尚未订阅 `Health.Died` 或显示失败提示。
- `GameOverUI` 已在成功缓存玩家 Health 的同一查找块中单次订阅 `Died`，私有显示方法负责激活 Panel，`OnDisable()` 成对退订；已避免把 `+=` 放在每帧执行的普通 Update 路径中造成重复订阅。用户完成失败流程 Play 验收并反馈“没问题”：初始 Panel 隐藏、玩家死亡时立即显示、Dead 后不能移动或开火、按 R 重载后 Panel 重新隐藏且新玩家恢复，Unity Console 无错误或警告。下一步补测 Dodge 免伤及受击后 0.5 秒无敌，再做四状态综合回归与清理。
- Dodge 免伤 Play 回归通过。用户确认翻滚进入敌人攻击范围时，敌人的首次攻击尝试不会触发 Hurt；该次被拒绝的攻击仍消耗 1 秒敌人冷却，翻滚结束并留在范围后约 1 秒才正常进入 Hurt；Console 无红色错误。下一步使用 Play 模式临时缩短敌人攻击冷却，压力验证玩家受击后 0.5 秒无敌。
- Codex 已完成玩家受击后无敌的运行时压力测试：仅在 Play 模式把 SlimeBlock 攻击冷却临时改为 0.1 秒并将敌人置于玩家 0.5 单位内，订阅玩家 `HealthChanged/Died` 记录时间。有效生命变化依次发生在 t=0.080、0.640、1.200、1.780、2.320、2.880、3.400、3.960、4.500、5.060 秒，相邻间隔约 0.54—0.58 秒，证明敌人虽每 0.1 秒尝试攻击，玩家 0.5 秒受击无敌会拒绝窗口内的攻击。生命归零时 `Died` 仅在 t=5.060 发布一次，继续等待后日志总数保持不变；退出 Play 后运行时改动未保存，脚本默认攻击冷却仍为 1 秒，Console 无错误或警告。
- 2026-08-28 今日功能完成条件已有完整证据：正式 SlimeBlock 复用 `IDamageable/Health`；`Dormant → Chase ⇄ Attack` 与任意存活流程进入 `Dead` 均已分别运行验证；攻击距离 1.2、攻击冷却 1 秒、伤害 10 生效；敌人死亡停止行动且单次发布 Died；玩家 Hurt、Dodge 免伤、受击后 0.5 秒无敌、Dead、失败提示和 R 重开均通过。当前剩余仅为非阻断清理/手感项：未使用的旧 `stopDistance` 字段、查找玩家的临时日志，以及以后再做的攻击起手提示/动画命中帧。
